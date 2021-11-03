using System.ComponentModel.DataAnnotations;

namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthTokenRequest
    {
        [Required]
        [StringLength(128)]
        public string LinkToken { get; set; }
    }
}
