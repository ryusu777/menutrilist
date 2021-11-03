namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthVerifyEmailRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
