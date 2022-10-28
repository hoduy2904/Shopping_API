using Newtonsoft.Json;

namespace ShoppingAPI.Data.Models
{
    public class ResultApi
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string[]? Message { get; set; }
        public object? Data { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
