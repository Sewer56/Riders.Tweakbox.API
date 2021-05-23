using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser
{
    public class PostServerRequest : IEquatable<PostServerRequest>
    {
        /// <summary>
        /// Name of the server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The port the host is listening at.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The type associated with this server.
        /// </summary>
        public MatchTypeDto Type { get; set; }

        /// <summary>
        /// True if there is a password in the hosted game.
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// A list of semicolon ';' separated mods.
        /// In alphabetical order (ordinal ignore case).
        /// </summary>
        public string Mods { get; set; }

        /// <summary>
        /// List of players from the server.
        /// </summary>
        public List<ServerPlayerInfoResult> Players { get; set; }

        /// <summary>
        /// Sorts a given array of mods and adds the result into the `Mods` string.
        /// Allows you to concatenate a set of mods.
        /// </summary>
        public void SetMods(string[] mods)
        {
            Array.Sort(mods, StringComparer.OrdinalIgnoreCase);
            Mods = string.Join(';', mods);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public bool Equals(PostServerRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name 
                   && Port == other.Port
                   && Type == other.Type
                   && HasPassword == other.HasPassword 
                   && Mods == other.Mods 
                   && Players.ListsEqual(other.Players);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PostServerRequest) obj);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Port, (int) Type, HasPassword, Mods);
        }
    }
}
