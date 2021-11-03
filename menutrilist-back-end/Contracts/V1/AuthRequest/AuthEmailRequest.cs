using System.ComponentModel.DataAnnotations;

namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthEmailRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }
    }
}
