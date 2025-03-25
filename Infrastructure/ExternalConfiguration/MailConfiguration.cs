namespace Persistence.OtherConfiguration
{
    public class MailConfiguration
    {
        public string Server { get; set; } = null!;
        public int Port { get; set; } = -1;
        public string Key { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmationPath { get; set; } = null!;
        public string ResetPhonePath { get; set; } = null!;
        public string RestPasswordPath { get; set; } = null!;
    }
}
