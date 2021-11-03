using Menutrilist.Contracts.V1.Requests;
using Menutrilist.Contracts.V1.Responses;
using Menutrilist.Data;
using Menutrilist.Domain.Identity;
using Menutrilist.Helpers;
using Menutrilist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Menutrilist.Repository.Identity
{
    public class IdentityRepo : IIdentityRepo
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly MenutrilistContext _context;
        private readonly JwtOptions _identitySettings;
        private readonly IEmailService _emailService;

        public IdentityRepo(
            UserManager<AspNetUser> userManager, 
            JwtOptions jwtSettings, 
            TokenValidationParameters tokenValidationParameters, 
            MenutrilistContext context, 
            RoleManager<AspNetRole> roleManager, 
            IEmailService emailService
        )
        {
            _context = context;
            _identitySettings = jwtSettings;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenValidationParameters = tokenValidationParameters;
            _emailService = emailService;
        }

        public async Task<AuthUserResponse> GetUserResponse(int userId)
        {
            var user = await _context.AspNetUsers.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            var userResponse = new AuthUserResponse
            {
                UserName = user.UserName,
                Email = user.Email,
            };
            return userResponse;
        }

        public async Task<AuthResultResponse> SignUpAsync(AuthSignupRequest model, string origin)
        {
            // Check username existing
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                return new AuthResultResponse(new[] { "Username already taken, please use another username" });
            }
            // Check email existing
            user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new AuthResultResponse(new[] { "Email has been used, please use another email" });
            }

            var newUser = new AspNetUser
            {
                Email = model.Email,
                UserName = model.Username,
                BirthDate = model.BirthDate,
                EmailConfirmed = _identitySettings.AutoEmailConfirmed
            };

            var createdUser = await _userManager.CreateAsync(newUser, model.Password);

            AuthResultResponse authResult = new AuthResultResponse
            {
                Succeeded = createdUser.Succeeded
            };
            if (createdUser.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var link = await GenerateSecurityTokenLink(1, newUser.Id, token);
                authResult.User = new AuthUserResponse
                {
                    UserId = newUser.Id,
                    UserName = newUser.UserName,
                    Email = newUser.Email
                };

                if (_identitySettings.SendEmailVerification)
                {
                    authResult.Message = "You account has been registered. Please check your email for email confirmation.";
                    await SendVerificationEmailAsync(newUser.Email, link, origin);
                }
            }
            else
            {
                authResult.Errors = createdUser.Errors.Select(x => x.Description);
            }
            return authResult;
        }

        public async Task<AuthResultResponse> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(username);
                if (user == null)
                {
                    return new AuthResultResponse
                    {
                        Errors = new[] { "Incorrect username or password" }
                    };
                }
            }

            if (_identitySettings.RequiredEmailConfirmed)
            {
                var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                if (!isEmailConfirmed)
                {
                    return new AuthResultResponse
                    {
                        Errors = new[] { "Please confirmed your email" }
                    };
                }
            }

            var isLockedOut = await _userManager.IsLockedOutAsync(user);
            if (isLockedOut)
            {
                return new AuthResultResponse
                {
                    Errors = new[] { "The account was locked out" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            
            if (!userHasValidPassword)
            {
                await _userManager.AccessFailedAsync(user);
                return new AuthResultResponse
                {
                    Errors = new[] { "Incorrect username or password" }
                };
            }
            await _userManager.ResetAccessFailedCountAsync(user);

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthResultResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthResultResponse { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthResultResponse { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.AspNetRefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthResultResponse { Errors = new[] { "This refresh token does not exist" } };
            }

            if (storedRefreshToken.IsExpired)
            {
                return new AuthResultResponse { Errors = new[] { "This refresh token has expired" } };
            }

            if (!storedRefreshToken.IsActive)
            {
                return new AuthResultResponse { Errors = new[] { "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthResultResponse { Errors = new[] { "This refresh token does not match this JWT" } };
            }

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "user-id").Value);
            storedRefreshToken.RevokedDate = DateTime.UtcNow;
            _context.AspNetRefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthResultResponse> GenerateAuthenticationResultForUserAsync(AspNetUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_identitySettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("user-id", user.Id.ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_identitySettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new AspNetRefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiredDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.AspNetRefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResultResponse
            {
                Succeeded = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
                User = new AuthUserResponse
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                },
                Message = "Login success"
            };
        }


        public async Task<AuthTokenResponse> GetLinkTokenAsync(string linkToken)
        {
            var authTokenResponse = new AuthTokenResponse();

            var secToken = await _context.AspNetLinkTokens.SingleOrDefaultAsync(x => (x.LinkCode == linkToken && x.Status == 0));
            if (secToken == null)
            {
                authTokenResponse.Succeeded = false;
                authTokenResponse.Errors = new[] { "Invalid link token" };
            }
            else
            {
                authTokenResponse.Succeeded = true;
                authTokenResponse.UserId = secToken.UserId;
                authTokenResponse.Token = secToken.Token;
            }
            return authTokenResponse;
        }

        public async Task<AuthResultResponse> ResendConfirmEmailAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthResultResponse(new[] { "Email does not registered" });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = await GenerateSecurityTokenLink(1, user.Id, token);
            var authResult = new AuthResultResponse
            {
                Token = null,
                RefreshToken = null,
                User = null,
                Succeeded = true,
                Message = "Please check your email to confirm",
                Errors = null
            };
            await SendVerificationEmailAsync(email, link, origin);

            return authResult;
        }

        public async Task<AuthResultResponse> VerifyEmailAsync(AuthVerifyEmailRequest model, string origin)
        {
            if (model.Token == null)
            {
                // always return ok response to prevent email enumeration
                return new AuthResultResponse { Errors = new[] { "Invalid email verify token" } };
            }
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                // always return ok response to prevent email enumeration
                return new AuthResultResponse { Errors = new[] { "Invalid email verify token" } };
            }

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            var authResult = new AuthResultResponse
            {
                Succeeded = result.Succeeded
            };
            if (result.Succeeded)
            {
                await UpdateLinkTokenStatus(user.Id, model.Token);
                authResult.Message = "Email verified";
            }
            else
            {
                authResult.Errors = result.Errors.Select(x => x.Description);
            }
            return authResult;
        }

        public async Task<AuthResultResponse> ForgotPasswordAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                // always return ok response to prevent email enumeration
                return new AuthResultResponse { Errors = new[] { "Email does not registered" } };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = await GenerateSecurityTokenLink(2, user.Id, token);

            if (!await SendResetPasswordEmailAsync(email, link, origin))
            {
                return new AuthResultResponse { Errors = new[] { "Failed to send email" } };
            };

            var authResult = new AuthResultResponse
            {
                Token = null,
                RefreshToken = null,
                User = null,
                Succeeded = true,
                Message = "Please check your email to reset password",
                Errors = null
            };
            return authResult;
        }

        public async Task<AuthResultResponse> ResetPasswordAsync(AuthResetPasswordRequest model, string origin)
        {
            if (model.Token == null)
            {
                // always return ok response to prevent email enumeration
                return new AuthResultResponse { Errors = new[] { "Reset password failed" } };
            }
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                // always return ok response to prevent email enumeration
                return new AuthResultResponse { Errors = new[] { "Reset password failed" } };
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResultResponse(result.Errors.Select(x => x.Description));
            }
            await UpdateLinkTokenStatus(user.Id, model.Token);
            return new AuthResultResponse
            {
                Succeeded = true,
                Message = "Your password already reset to new password"
            };
        }

        private string RandomTokenString(int tokenLength = 40)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[tokenLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> GenerateSecurityTokenLink(short tokenType, int userId, string token)
        {
            string link = RandomTokenString();
            var secToken = new AspNetLinkToken
            {
                LinkCode = link,
                TokenType = tokenType,
                UserId = userId,
                Token = token
            };
            await _context.AspNetLinkTokens.AddAsync(secToken);
            await _context.SaveChangesAsync();
            return link;
        }

        private async Task<bool> UpdateLinkTokenStatus(int userId, string token)
        {
            var secToken = await _context.AspNetLinkTokens.SingleOrDefaultAsync(x => (x.UserId == userId && x.Token == token && x.Status == 0));
            if (secToken == null)
            {
                return false;
            }

            secToken.Status = 1; // set to 'used token'
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> SendVerificationEmailAsync(string email, string token, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}email-verification/{token}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">Verify Email</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>email-verification</code> api route:</p>
                             <p>{token}</p>";
            }
            var emailSent = await _emailService.SendAsync(
                to: email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
            return emailSent;
        }

        private async Task<bool> SendResetPasswordEmailAsync(string email, string token, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}reset-password/{token}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 hour:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/reset-password</code> route:</p>
                             <p><code>{token}</code></p>";
            }

            var emailSent = await _emailService.SendAsync(
                to: email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );

            return emailSent;
        }

        public async Task<ResponseWrappers<bool>> UpdateUserAsync(int id, UserUpdateRequest adminUser)
        {
            if (id != adminUser.Id)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Bad request.",
                    Errors = new[] { "Bad request." }
                };
            }

            var rec = await _context.AspNetUsers.FindAsync(id);
            if (rec == null)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 404,
                    Message = "Record not found.",
                    Errors = new[] { "Record not found." }
                };
            }

            if ((rec.UserName.ToUpper() != adminUser.UserName.ToUpper()) || (rec.Email.ToUpper() != adminUser.Email.ToUpper()))
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Data not valid.",
                    Errors = new[] { "Data not valid." }
                };
            }

            rec.FullName = adminUser.FullName;
            rec.BirthDate = adminUser.BirthDate;

            _context.Entry(rec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!SecUserExists(id))
                {
                    return new ResponseWrappers<bool>
                    {
                        Succeded = false,
                        Status = 404,
                        Message = "Record not found.",
                        Errors = new[] { "Record not found." }
                    };
                }
                else
                {
                    return new ResponseWrappers<bool>
                    {
                        Succeded = false,
                        Status = 500,
                        Message = e.Message,
                        Errors = new[] { e.Message }
                    };
                }
            }

            return new ResponseWrappers<bool>
            {
                Succeded = true,
                Status = 200,
            };
        }

        public async Task<ResponseWrappers<bool>> DeleteUserAsync(int id)
        {
            var rec = await _context.AspNetUsers.FindAsync(id);
            if (rec == null)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 404,
                    Message = "Record not found.",
                    Errors = new[] { "Record not found." }
                };
            }

            _context.AspNetUsers.Remove(rec);
            await _context.SaveChangesAsync();

            return new ResponseWrappers<bool>
            {
                Succeded = true,
                Status = 200,
                Data = true
            };
        }

        public async Task<ResponseWrappers<bool>> AddUserToRole(UserRoleRequest adminUserRole)
        {
            var user = await _userManager.FindByNameAsync(adminUserRole.UserName);
            if (user == null)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 404,
                    Message = "Record not found.",
                    Errors = new[] { "Record not found." }
                };
            }
            if (user.Id != adminUserRole.UserId)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Bad request.",
                    Errors = new[] { "Bad request." }
                };
            }

            var roleExist = await _roleManager.RoleExistsAsync(adminUserRole.RoleName);
            if (!roleExist)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Role not valid.",
                    Errors = new[] { "Role not valid." }
                };
            }

            var identityResult = await _userManager.AddToRoleAsync(user, adminUserRole.RoleName);
            if (!identityResult.Succeeded)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Failed to assign user role.",
                    Errors = identityResult.Errors.Select(x => x.Description)
                };
            }

            return new ResponseWrappers<bool>
            {
                Succeded = true,
                Status = 200,
            };
        }

        public async Task<ResponseWrappers<bool>> RemoveUserFromRole(UserRoleRequest adminUserRole)
        {
            var user = await _userManager.FindByNameAsync(adminUserRole.UserName);
            if (user == null)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 404,
                    Message = "Record not found.",
                    Errors = new[] { "Record not found." }
                };
            }
            if (user.Id != adminUserRole.UserId)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Bad request.",
                    Errors = new[] { "Bad request." }
                };
            }

            var identityResult = await _userManager.RemoveFromRoleAsync(user, adminUserRole.RoleName);
            if (!identityResult.Succeeded)
            {
                return new ResponseWrappers<bool>
                {
                    Succeded = false,
                    Status = 400,
                    Message = "Failed to assign user role.",
                    Errors = identityResult.Errors.Select(x => x.Description)
                };
            }

            return new ResponseWrappers<bool>
            {
                Succeded = true,
                Status = 200,
            };
        }


        private bool SecUserExists(int id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
