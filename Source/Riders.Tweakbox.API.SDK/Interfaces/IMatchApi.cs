using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.SDK.Common;

namespace Riders.Tweakbox.API.SDK.Interfaces
{
    [Headers(RefitConstants.BearerAuthentication)]
    public interface IMatchApi
    {
        /// <summary>
        /// Retrieves all matches from the database.
        /// </summary>
        /// <response code="200">Success</response>
        [Get("/" + Routes.Match.Base + "/" + Routes.RestGetAll)]
        public Task<ApiResponse<PagedResponse<GetMatchResult>>> GetAll([Query] PaginationQuery query = null);

        /// <summary>
        /// Retrieves a single match from the database.
        /// </summary>
        /// <param name="id">Unique identifier for the match.</param>
        /// <response code="400">Requested match was not found.</response>
        [Get("/" + Routes.Match.Base + "/" + Routes.RestGet)]
        public Task<ApiResponse<GetMatchResult>> Get(int id);

        /// <summary>
        /// Updates a single item in the resource. [Admin Only]
        /// </summary>
        /// <param name="id">Unique identifier for the match.</param>
        /// <param name="item">Altered details of the match.</param>
        /// <response code="400">Requested match was not found.</response>
        /// <response code="200">Success</response>
        [Put("/" + Routes.Match.Base + "/" + Routes.RestUpdate)]
        public Task<ApiResponse<bool>> Update(int id, [Body] PostMatchRequest item);

        /// <summary>
        /// Adds a new match to the database.
        /// </summary>
        /// <param name="item">The details of the match.</param>
        /// <response code="201">Successfully created.</response>
        [Post("/" + Routes.Match.Base + "/" + Routes.RestCreate)]
        public Task<ApiResponse<GetMatchResult>> Create([Body] PostMatchRequest item);

        /// <summary>
        /// Deletes a single match from the database. [Admin Only]
        /// </summary>
        /// <param name="id">Unique identifier for the match.</param>
        /// <response code="400">Requested match was not found.</response>
        /// <response code="200">Success</response>
        [Delete("/" + Routes.Match.Base + "/" + Routes.RestDelete)]
        public Task<ApiResponse<bool>> Delete(int id);
    }
}