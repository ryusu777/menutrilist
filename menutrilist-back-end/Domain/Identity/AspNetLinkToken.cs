using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menutrilist.Domain.Identity
{
    public class AspNetLinkToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string LinkCode { get; set; }
        public int TokenType { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; }
        public DateTimeOffset? ExpiredTime { get; set; }
        public int Status { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AspNetUser User { get; set; }
    }
}