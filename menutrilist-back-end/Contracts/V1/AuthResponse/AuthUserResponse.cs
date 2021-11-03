namespace Menutrilist.Contracts.V1.Responses
{
    public class AuthUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public short EntityType { get; set; }
        public string EntityCode { get; set; }
    }
}
