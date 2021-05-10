using System;
using System.Net;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IServerBrowserService
    {
        /// <summary>
        /// Gets all servers stored.
        /// </summary>
        GetAllServerResult GetAll();

        /// <summary>
        /// Creates a new server (or refreshes existing one) in the database.
        /// </summary>
        ServerCreatedResult CreateOrRefresh(IPAddress source, PostServerRequest item);

        /// <summary>
        /// Deletes a server from the list.
        /// </summary>
        bool Delete(IPAddress source, int port, Guid id);
    }
}