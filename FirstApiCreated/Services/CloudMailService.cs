namespace FirstApiCreated.Services
{
    public class CloudMailService : IMailService
    {

        private readonly String _mailTo = String.Empty;
        private readonly String _mailFrom = String.Empty;
        public CloudMailService (IConfiguration configuration)
        {
            _mailTo = configuration["MailService : ToMail"];
            _mailFrom = configuration["MailService : FromMail"];
        }
        public void Send(String subject, String message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}  with {nameof(CloudMailService)} ");
            Console.WriteLine($"Subject  : {subject}");
            Console.WriteLine($"Message : {message}");

        }
    }
}
