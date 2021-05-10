using System;
using System.ComponentModel.DataAnnotations;

namespace Riders.Tweakbox.API.Domain.Models.Database
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }
    }
}
