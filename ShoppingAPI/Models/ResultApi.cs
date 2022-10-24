namespace ShoppingAPI.Models
{
    public class ResultApi
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
