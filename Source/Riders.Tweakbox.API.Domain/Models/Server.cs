using System;

namespace Riders.Tweakbox.API.Domain.Models
{
    /// <summary>
    /// Server: Stored in-memory as opposed to database.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Name of the server.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Name of the player hosting.
        /// </summary>
        public string HostName   { get; set; }

        /// <summary>
        /// Last time server info was refreshed.
        /// </summary>
        public DateTime LastRefresh   { get; set; }
    }
}