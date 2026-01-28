namespace dietsync.DTOs
{
    public class AuthErrorDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public LoginResponseDto? Data { get; set; }
    }
}
