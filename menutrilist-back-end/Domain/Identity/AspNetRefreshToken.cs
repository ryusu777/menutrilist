using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menutrilist.Domain.Identity
{
    public class AspNetRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Token { get; set; }
        [Required]
        [StringLength(256)]
        public string JwtId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
        public DateTimeOffset? RevokedDate { get; set; }
        [StringLength(256)]
        public string RevokedFrom { get; set; }
        public bool Invalidated { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        [StringLength(256)]
        public string CreatedFrom { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiredDate;
        public bool IsActive => RevokedDate == null && !IsExpired;

        [ForeignKey(nameof(UserId))]
        public virtual AspNetUser User { get; set; }
    }
}