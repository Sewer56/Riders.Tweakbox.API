﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Riders.Tweakbox.API.Domain.Models.Memory
{
    public class ServerPlayerInfo : IEquatable<ServerPlayerInfo>
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Latency of the player.
        /// </summary>
        public int Latency { get; set; }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public bool Equals(ServerPlayerInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Latency == other.Latency;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ServerPlayerInfo) obj);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Latency);
        }
    }
}
