namespace Riders.Tweakbox.API.Application.Models.Config
{
    public class MailSettings
    {
        public string Host     { get; set; }
        public int Port        { get; set; }
        public bool EnableSSL  { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
