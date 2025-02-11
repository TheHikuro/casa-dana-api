namespace CasaDanaAPI.Infrastructure.Configuration
{
    public class EmailSettings
    {
        public required string Host { get; init; }
        public int Port { get; init; }
        public bool EnableSsl { get; init; }
        public required string SenderName { get; init; }
        public required string SenderEmail { get; init; }
        public required string SenderPassword { get; init; }
    }
}
