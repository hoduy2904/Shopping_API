using Newtonsoft.Json;

namespace ShoppingAPI.Common.Models
{
    public class ResponseApi
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string[]? Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
        public DateTime Created { get; set; } = DateTime.Now;
        public object? Data { get; set; }
    }
}
