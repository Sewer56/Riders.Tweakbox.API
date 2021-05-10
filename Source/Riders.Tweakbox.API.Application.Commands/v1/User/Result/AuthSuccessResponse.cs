namespace Riders.Tweakbox.API.Application.Commands.v1.User.Result
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
