namespace Emertec.UI.Domain.Comman
{
    public class CommanResponseDto
    {
        public int? StatusCode { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
