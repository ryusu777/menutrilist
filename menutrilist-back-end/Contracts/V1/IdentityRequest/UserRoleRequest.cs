namespace Menutrilist.Contracts.V1.Requests
{
    public class UserRoleRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}