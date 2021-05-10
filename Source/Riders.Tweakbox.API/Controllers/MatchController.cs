using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Controllers.Common;
using Riders.Tweakbox.API.Domain.Common;

namespace Riders.Tweakbox.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.User)]
    [ApiController]
    [Route(Routes.Match.Base)]
    [ApiExplorerSettings(GroupName = "v1")]
    public class MatchController : RestControllerBase<GetMatchResult, PostMatchRequest>
    {
        private IMatchService _service;

        /// <inheritdoc />
        public MatchController(IMatchService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all matches from the database.
        /// </summary>
        public override async Task<ActionResult<List<GetMatchResult>>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAll(cancellationToken);
        }

        /// <summary>
        /// Retrieves a single match from the database.
        /// </summary>
        /// <param name="id">Unique identifier for the match.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">Requested match was not found.</response>
        public override async Task<ActionResult<GetMatchResult>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.Get(id, cancellationToken);
            if (result == null)
                return NotFound();

            return result;
        }

        /// <summary>
        /// Updates a single item in the resource. [Admin Only]
        /// </summary>
        /// <param name="id">Unique identifier for the match.</param>
        /// <param name="item">Altered details of the match.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">Requested match was not found.</response>
        /// <response code="204">Success (No Content)</response>
        [Authorize(Roles = Roles.Admin)]
        public override async Task<IActionResult> Update(int id, PostMatchRequest item, CancellationToken cancellationToken)
        {
            var result = await _service.Update(id, item, cancellationToken);
            if (result == false)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Adds a new match to the database.
        /// </summary>
        /// <param name="item">The details of the match.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">Successfully created.</response>
        public override async Task<ActionResult<GetMatchResult>> Create(PostMatchRequest item, CancellationToken cancellationToken)
        {
            var result = await _service.Create(item, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Deletes a single match from the database. [Admin Only]
        /// </summary>
        /// <param name="id">Unique identifier for the match.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">Requested match was not found.</response>
        /// <response code="204">Success (No Content)</response>
        [Authorize(Roles = Roles.Admin)]
        public override async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _service.Delete(id, cancellationToken);
            if (result == false)
                return NotFound();

            return NoContent();
        }
    }
}
