using System;
using System.Net;

namespace Riders.Tweakbox.API.Domain.Models.Memory
{
    public struct ServerAddressPortPair : IEquatable<ServerAddressPortPair>
    {
        public IPAddress Address;
        public int Port;

        /// <inheritdoc />
        public bool Equals(ServerAddressPortPair other) => Equals(Address, other.Address) && Port == other.Port;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is ServerAddressPortPair other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Address, Port);
    }
}
