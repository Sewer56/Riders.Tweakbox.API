using System;
using System.Collections.Generic;

namespace Riders.Tweakbox.API.Domain.Models.Memory
{
    public class ServerInfo
    {
        /// <summary>
        /// ID that is unique to this server.
        /// Used for controlling the deletion of the server.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The last time this server was refreshed.
        /// </summary>
        public DateTime LastRefreshTime { get; set; }

        /// <summary>
        /// IP Address of the Server Host.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Name of the server.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The port the host is listening on.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The type associated with this server.
        /// </summary>
        public MatchType Type { get; set; }

        /// <summary>
        /// The country associated with this server.
        /// Derived from the IP Address.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Mods running on this server.
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Mods running on this server.
        /// </summary>
        public string Mods { get; set; }

        /// <summary>
        /// List of players from the server.
        /// </summary>
        public List<ServerPlayerInfo> Players { get; set; }
    }
}
