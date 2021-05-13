using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Riders.Tweakbox.API.Application.Commands;

#pragma warning disable 1998

namespace Riders.Tweakbox.API.Controllers.Common
{
    /// <summary>
    /// Provides a base class for implementing RESTful controllers.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class RestControllerBase<TGetType, TPostType> : ControllerBase
    {
        /// <summary>
        /// Retrieves all items from the resource.
        /// </summary>
        [HttpGet(Routes.RestGetAll)]
        public virtual async Task<ActionResult<List<TGetType>>> GetAll(CancellationToken cancellationToken) => throw new NotImplementedException();

        /// <summary>
        /// Retrieves a single item from the resource.
        /// </summary>
        [HttpGet(Routes.RestGet)]
        public virtual async Task<ActionResult<TGetType>> Get(int id, CancellationToken cancellationToken) => throw new NotImplementedException();

        /// <summary>
        /// Updates a single item in the resource.
        /// </summary>
        [HttpPut(Routes.RestUpdate)]
        public virtual async Task<IActionResult> Update(int id, TPostType item, CancellationToken cancellationToken) => throw new NotImplementedException();

        /// <summary>
        /// Adds a new item to the resource.
        /// </summary>
        [HttpPost(Routes.RestCreate)]
        public virtual async Task<ActionResult<TGetType>> Create(TPostType item, CancellationToken cancellationToken) => throw new NotImplementedException();

        /// <summary>
        /// Deletes an item from the resource.
        /// </summary>
        [HttpDelete(Routes.RestDelete)]
        public virtual async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
