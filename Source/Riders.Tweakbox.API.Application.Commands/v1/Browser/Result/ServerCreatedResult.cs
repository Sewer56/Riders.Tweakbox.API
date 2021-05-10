using System;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser.Result
{
    public class ServerCreatedResult : GetServerResult
    {
        /// <summary>
        /// ID that is unique to this server.
        /// Used for controlling the deletion of the server.
        /// You get a new Id for each request returned.
        /// </summary>
        public Guid Id { get; set; }
    }
}
