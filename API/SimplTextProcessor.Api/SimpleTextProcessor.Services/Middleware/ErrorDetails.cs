namespace SimpleTextProcessor.Services.Middleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Details { get; set; } = default!;
    }
}
