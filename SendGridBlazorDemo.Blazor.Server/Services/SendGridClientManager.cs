using DevExpress.ExpressApp;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace SendGridBlazorDemo.Blazor.Server.Services
{
    public class SendGridClientManager
    {
        private static SendGridClientManager _instance;
        private static readonly object _lock = new object();
        private SendGridClient _client;
        public IConfiguration configuration { get; set; }

        // SendGrid API Key
        private string ApiKey { get; set; } = "EMPTY";

        // Constructor is private so it can't be instantiated outside of this class
        private SendGridClientManager(IConfiguration configuration)
        {
            if (!ModuleHelper.IsDesignMode)
            {
                ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ?? configuration["SENDGRID:API_KEY"] ?? "";

            }
            else
            {
                ApiKey = "EMPTY";
            }
            _client = new SendGridClient(ApiKey);
        }

        // The public instance property to access the singleton instance
        public static SendGridClientManager GetInstance(IConfiguration configuration)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SendGridClientManager(configuration);
                }
                return _instance;
            }
        }

        public SendGridClient Client => _client;

        public async Task<bool> SendEmail(string to, string cc, string bcc, string htmlContent)
        {
            SendGridMessage msg = new SendGridMessage();
            EmailAddress fromAddress = new EmailAddress("oniel.mato@xari.io", "Blazor Demo");
            List<EmailAddress> recipients = new List<EmailAddress> { new EmailAddress(to, to) };

            msg.SetSubject("A new user has registered");
            msg.SetFrom(fromAddress);
            msg.AddTos(recipients);
            msg.AddCc(cc??"", cc??"");
            msg.AddBcc(bcc??"", bcc??"");
            msg.HtmlContent = htmlContent??"";

            Response response = await _client.SendEmailAsync(msg);
            if (Convert.ToInt32(response.StatusCode) >= 400)
            {
                return false;
            }
            return true;
        }
    }
}
