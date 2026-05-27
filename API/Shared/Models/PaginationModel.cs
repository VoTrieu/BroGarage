namespace BroGarage.API.Shared.Models
{
    public class PaginationModel<T>
    {
        public int TotalRow { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public T? Data { get; set; }

        /// <summary>
        /// Current sort field
        /// </summary>
        public string? OrderBy { get; set; }

        /// <summary>
        /// Current sort direction (asc or desc)
        /// </summary>
        public string SortDirection { get; set; } = "desc";
    }
}
