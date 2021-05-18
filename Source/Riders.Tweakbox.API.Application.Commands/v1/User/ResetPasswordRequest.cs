using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riders.Tweakbox.API.Application.Commands.v1.User
{
    /// <summary>
    /// Encapsulates an individual request to reset a password.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// The password for which to reset the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The returned reset token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The new password to set for the user.
        /// </summary>
        public string Password { get; set; }
    }
}
