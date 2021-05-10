using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Riders.Tweakbox.API.Domain.Models.Database;

namespace Riders.Tweakbox.API.Infrastructure.Common
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Player> Players                    { get; set; }
        public DbSet<Match> Matches                     { get; set; }
        public DbSet<PlayerRaceDetails> RaceDetails     { get; set; }
        public DbSet<RefreshToken> RefreshTokens        { get; set; }

        /// <inheritdoc />
        public ApplicationDbContext() { }

        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
