using System;

namespace Riders.Tweakbox.API.Application.Commands.v1.User.Result
{
    public class AuthSuccessResponse : IEquatable<AuthSuccessResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        /// <inheritdoc />
        public bool Equals(AuthSuccessResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Token == other.Token && RefreshToken == other.RefreshToken;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AuthSuccessResponse) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Token, RefreshToken);
        }
    }
}
