namespace Riders.Tweakbox.API.Application.Commands.v1.User
{
    public class UserLoginRequest
    {
        /// <summary>
        /// The user name of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
