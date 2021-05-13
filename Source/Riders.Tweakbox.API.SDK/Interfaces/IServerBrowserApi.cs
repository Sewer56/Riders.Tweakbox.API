using System;
using System.Threading.Tasks;
using Refit;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;

namespace Riders.Tweakbox.API.SDK.Interfaces
{
    public interface IServerBrowserApi
    {
        /// <summary>
        /// Retrieves a list of all active servers that are in-lobby.
        /// </summary>
        /// <response code="200">Success</response>
        [Get("/" + Routes.Browser.Base + "/" + Routes.RestGetAll)]
        public Task<ApiResponse<GetAllServerResult>> GetAll();

        /// <summary>
        /// Creates a new server entry or refreshes an existing entry.
        /// You should call this endpoint approximately around every 60 seconds for as long
        /// as your server is ready to accept users.
        /// </summary>
        /// <param name="item">
        ///     Details of the server in question.
        ///     Server assumes the IP of the user making this request is the IP of the lobby.
        /// </param>
        /// <response code="200">Success</response>
        [Post("/" + Routes.Browser.Base + "/" + Routes.RestCreate)]
        public Task<ApiResponse<ServerCreatedResult>> CreateOrRefresh([Body] PostServerRequest item);

        /// <summary>
        /// Deletes an existing server entry.
        /// </summary>
        /// <param name="id">
        ///     Unique ID returned by the Create (<seealso cref="CreateOrRefresh"/>) call.
        /// </param>
        /// <param name="port">The port under which the server is being hosted.</param>
        /// <response code="404">Server to delete not found.</response>
        /// <response code="200">Success</response>
        [Delete("/" + Routes.Browser.Base + "/" + Routes.RestDelete)]
        public Task<ApiResponse<bool>> Delete(Guid id, int port);
    }
}