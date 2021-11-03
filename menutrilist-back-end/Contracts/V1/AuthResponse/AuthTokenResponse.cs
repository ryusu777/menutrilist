using System.Collections.Generic;

namespace Menutrilist.Contracts.V1.Responses
{
    public class AuthTokenResponse
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
