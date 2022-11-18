namespace FirstApiCreated.Services
{
    public class CloudMailService : IMailService
    {
        private String _mailTo = new string("Company@email.com");
        private String _mailFrom = new string("NoReply@email.com");
        public void Send(String subject, String message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}  with {nameof(CloudMailService)} ");
            Console.WriteLine($"Subject  : {subject}");
            Console.WriteLine($"Message : {message}");

        }
    }
}
