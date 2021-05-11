namespace Riders.Tweakbox.API.Application.Commands.v1.User
{
    public class UserRegistrationRequest
    {
        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}