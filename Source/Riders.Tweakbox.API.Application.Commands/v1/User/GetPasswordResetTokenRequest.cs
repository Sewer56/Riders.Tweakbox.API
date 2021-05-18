namespace Riders.Tweakbox.API.Application.Commands.v1.User
{
    /// <summary>
    /// Represents an individual request to get a token to reset an individual user's password.
    /// </summary>
    public class GetPasswordResetTokenRequest
    {
        /// <summary>
        /// The email for which to reset the password.
        /// </summary>
        public string Email { get; set; }
    }
}
