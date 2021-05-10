using System.Collections.Generic;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser
{
    public class PostServerRequest
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
        /// List of players from the server.
        /// </summary>
        public List<ServerPlayerInfoResult> Players { get; set; }
    }
}
