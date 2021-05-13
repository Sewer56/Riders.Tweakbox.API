using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Riders.Tweakbox.API.Application.Commands;

namespace Riders.Tweakbox.API.Application.Services.Base
{
    public interface IRestService<TGetItem, TPostItem>
    {
        /// <summary>
        /// Gets all items stored in the database.
        /// </summary>
        Task<List<TGetItem>> GetAll(PaginationQuery paginationQuery, CancellationToken token);

        /// <summary>
        /// Get a single items stored in the database.
        /// </summary>
        Task<TGetItem> Get(int id, CancellationToken token);

        /// <summary>
        /// Updates the contents of one items.
        /// </summary>
        Task<bool> Update(int id, TPostItem item, CancellationToken token);

        /// <summary>
        /// Creates a new items in the database.
        /// </summary>
        Task<TGetItem> Create(TPostItem item, CancellationToken token);

        /// <summary>
        /// Deletes a items from the database.
        /// </summary>
        Task<bool> Delete(int id, CancellationToken token);
    }
}