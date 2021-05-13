namespace Riders.Tweakbox.API.Application.Commands
{
    /// <summary>
    /// Allows to add pagination support to individual queries.
    /// </summary>
    public class PaginationQuery
    {
        /// <summary>
        /// Maximum allowed page size.
        /// </summary>
        public const int MaxPageSize = 100;

        /// <summary>
        /// Minimum allowed page size.
        /// </summary>
        public const int MinPageSize = 1;

        /// <summary>
        /// Minimum Page Number.
        /// </summary>
        public const int MinPageNumber = 0;

        /// <summary>
        /// Current number of the page.
        /// </summary>
        public int PageNumber { get; set; } = MinPageNumber;
        
        /// <summary>
        /// Number of results to return per page.
        /// Maximum Value is <seealso cref="MaxPageSize"/> (100)
        /// </summary>
        public int PageSize   { get; set; } = MaxPageSize;
    }

    /// <summary/>
    public static class PaginationQueryExtensions
    {
        /// <summary>
        /// Removes any out of bounds parameters in the pagination query.
        /// If the original query is null, returns a new object with the default values.
        /// </summary>
        public static PaginationQuery SanitizeOrDefault(this PaginationQuery query)
        {
            if (query == null)
                return new PaginationQuery();

            query.PageNumber = query.PageNumber < PaginationQuery.MinPageNumber ? 0 : query.PageNumber;
            query.PageSize   = query.PageSize   > PaginationQuery.MaxPageSize || query.PageSize < PaginationQuery.MinPageSize ? PaginationQuery.MaxPageSize : query.PageSize;
            return query;
        }
    }
}
