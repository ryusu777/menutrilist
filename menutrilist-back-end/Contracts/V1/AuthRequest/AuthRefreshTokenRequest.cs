namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthRefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
    }
}
