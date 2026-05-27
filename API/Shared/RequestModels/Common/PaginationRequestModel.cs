namespace BroGarage.API.Shared.RequestModels.Common
{
    /// <summary>
    /// Base pagination request model for standardizing pagination parameters across all endpoints
    /// </summary>
    public class PaginationRequestModel
    {
        /// <summary>
        /// Page size (number of items per page)
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page index (1-based)
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Optional: field name to sort by (e.g., "Name", "CreatedDate")
        /// </summary>
        public string? OrderBy { get; set; }

        /// <summary>
        /// Sort direction: "asc" for ascending, "desc" for descending. Default: "desc"
        /// </summary>
        public string SortDirection { get; set; } = "desc";

        /// <summary>
        /// Search keyword
        /// </summary>
        public string? Keyword { get; set; }
    }
}
