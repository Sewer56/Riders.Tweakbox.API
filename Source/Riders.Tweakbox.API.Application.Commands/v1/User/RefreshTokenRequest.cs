namespace Riders.Tweakbox.API.Application.Commands.v1.User
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
