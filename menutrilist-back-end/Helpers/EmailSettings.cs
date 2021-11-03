namespace Menutrilist.Helpers
{
    public class EmailSettings
    {
        public string EmailFrom { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }
        public string SecureSocketOptions { get; set; }
        public int RetryCount { get; set; }
    }
}
