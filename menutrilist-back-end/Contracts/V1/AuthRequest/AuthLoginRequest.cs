using System.ComponentModel.DataAnnotations;

namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthLoginRequest
    {
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        [Required]
        [StringLength(256)]
        public string Password { get; set; }
    }
}
