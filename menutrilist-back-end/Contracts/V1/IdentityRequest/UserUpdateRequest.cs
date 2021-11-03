using System;

namespace Menutrilist.Contracts.V1.Requests
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}