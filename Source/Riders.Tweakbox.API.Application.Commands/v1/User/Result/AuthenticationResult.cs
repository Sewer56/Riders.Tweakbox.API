using System.Collections.Generic;

namespace Riders.Tweakbox.API.Application.Commands.v1.User.Result
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string RefreshToken { get; set; }
    }
}
