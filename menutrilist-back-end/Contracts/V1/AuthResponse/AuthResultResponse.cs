using System.Collections.Generic;

namespace Menutrilist.Contracts.V1.Responses
{
    public class AuthResultResponse
    {
        public AuthResultResponse()
        {
            this.Succeeded = false;
        }
        public AuthResultResponse(IEnumerable<string> errors)
        {
            this.Token = null;
            this.RefreshToken = null;
            this.User = null;
            this.Succeeded = false;
            this.Errors = errors;
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public AuthUserResponse User { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}