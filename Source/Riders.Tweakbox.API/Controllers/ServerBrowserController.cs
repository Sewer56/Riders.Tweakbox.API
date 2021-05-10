using System;
using Microsoft.AspNetCore.Mvc;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Services;

namespace Riders.Tweakbox.API.Controllers
{
    [ApiController]
    [Route(Routes.Browser.Base)]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ServerBrowserController : ControllerBase
    {
        private IServerBrowserService _browserService;
        private ICurrentUserService _currentUserService;

        /// <inheritdoc />
        public ServerBrowserController(IServerBrowserService browserService, ICurrentUserService currentUserService)
        {
            _browserService = browserService;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Retrieves a list of all active servers that are in-lobby.
        /// </summary>
        [HttpGet(Routes.RestGetAll)]
        public IActionResult GetAll()
        {
            return Ok(_browserService.GetAll());
        }
        
        /// <summary>
        /// Creates a new server entry or refreshes an existing entry.
        /// </summary>
        /// <param name="item">
        ///     Details of the server in question.
        ///     Server assumes the IP of the user making this request is the IP of the lobby.
        /// </param>
        [HttpPost(Routes.RestCreate)]
        public IActionResult CreateOrRefresh(PostServerRequest item)
        {
            return Ok(_browserService.CreateOrRefresh(_currentUserService.IpAddress, item));
        }

        /// <summary>
        /// Deletes an existing server entry.
        /// </summary>
        /// <param name="id">
        ///     Unique ID returned by the Create (<seealso cref="CreateOrRefresh"/>) call.
        /// </param>
        /// <param name="port">The port under which the server is being hosted.</param>
        [HttpDelete(Routes.RestDelete)]
        public IActionResult Delete(Guid id, int port)
        {
            var deleted = _browserService.Delete(_currentUserService.IpAddress, port, id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}
