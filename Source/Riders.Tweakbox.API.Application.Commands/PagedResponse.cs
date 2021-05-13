using System.Collections.Generic;

namespace Riders.Tweakbox.API.Application.Commands
{
    /// <summary>
    /// Paginated response which represents a slice of all items available.
    /// </summary>
    public class PagedResponse<T>
    {
        public PagedResponse() { }
        public PagedResponse(List<T> items) => Items = items;

        public PagedResponse(List<T> items, int pageNumber, int pageSize)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// All the items returned by this response.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Current number of the page.
        /// </summary>
        public int PageNumber     { get; set; }
        
        /// <summary>
        /// Number of results per page.
        /// </summary>
        public int PageSize       { get; set; }
    }
}
