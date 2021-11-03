namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthResetPasswordRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
