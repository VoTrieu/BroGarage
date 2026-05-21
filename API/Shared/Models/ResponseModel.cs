namespace BroGarage.API.Shared.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; } = null!;

        public T? Result { get; set; }

        public int Code { get; set; } = 200;
    }

    public class ResponseModel
    {
        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; } = null!;

        public object? Result { get; set; }

        public int Code { get; set; } = 200;
    }
}
