using System;
using System.ComponentModel.DataAnnotations;

namespace Menutrilist.Contracts.V1.Requests
{
    public class AuthSignupRequest
    {
        [Required]
        [StringLength(256)]
        public string Username { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }
        [Required]
        [StringLength(256)]
        public string Password { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
