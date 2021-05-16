using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Riders.Tweakbox.API.Domain.Models.Database;

namespace Riders.Tweakbox.API.Infrastructure.Common
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Match> Matches                     { get; set; }
        public DbSet<PlayerRaceDetails> RaceDetails     { get; set; }
        public DbSet<RefreshToken> RefreshTokens        { get; set; }

        /// <inheritdoc />
        public ApplicationDbContext() { }

        /// <inheritdoc />
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // Keeping an open connection improves performance significantly with SQLite
            // The connection is closed when the context is disposed, that is, after the 
            // request is done since it is scoped.
            
            if (this.Database.IsSqlite())
            {
                this.Database.OpenConnection();
                this.Database.ExecuteSqlRaw("PRAGMA synchronous = '1';");
                this.Database.ExecuteSqlRaw("PRAGMA journal_mode = 'WAL';");
            }
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerRaceDetails>().HasKey(x => new {x.MatchId, x.PlayerId});
            base.OnModelCreating(builder);
        }
    }
}
