using Microsoft.AspNetCore.Identity;

namespace Riders.Tweakbox.API.Domain.Models.Database
{
    public class ApplicationRole : IdentityRole<int>
    {
        /// <inheritdoc />
        public ApplicationRole() {}

        /// <inheritdoc />
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
