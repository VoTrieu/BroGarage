namespace BroGarage.API.Shared.Models
{
    public class PaginationModel<T>
    {
        public int TotalRow { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public T? Data { get; set; }
    }
}
