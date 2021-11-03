using System.Collections.Generic;
using System.Threading.Tasks;
using Menutrilist.Contracts.V1.Requests;
using Menutrilist.Contracts.V1.Responses;
using Menutrilist.Domain.Identity;

namespace Menutrilist.Repository.Identity
{
    public interface IIdentityRepo
    {
        Task<AuthResultResponse> SignUpAsync(AuthSignupRequest model, string origin);
        Task<AuthResultResponse> AuthenticateAsync(string username, string password);
        Task<AuthResultResponse> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthTokenResponse> GetLinkTokenAsync(string linkToken);
        Task<AuthResultResponse> ResendConfirmEmailAsync(string email, string origin);
        Task<AuthResultResponse> VerifyEmailAsync(AuthVerifyEmailRequest model, string origin);
        Task<AuthResultResponse> ForgotPasswordAsync(string email, string origin);
        Task<AuthResultResponse> ResetPasswordAsync(AuthResetPasswordRequest model, string origin);
        Task<ResponseWrappers<bool>> UpdateUserAsync(int id, UserUpdateRequest adminUser);
        Task<ResponseWrappers<bool>> DeleteUserAsync(int id);
        Task<ResponseWrappers<bool>> AddUserToRole(UserRoleRequest adminUserRole);
        Task<ResponseWrappers<bool>> RemoveUserFromRole(UserRoleRequest adminUserRole);
    }
}
