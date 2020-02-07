using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    public class TestEmailClientProviderImplementation : IEmailClientProvider
    {
        public string[] Emails { get; private set; }
        public string Subject { get; private set; }
        public string MessageText { get; private set; }
        public string[] HiddenEmails { get; set; }

        public void Send(string[] emails, string[] hiddenEmails, string subject, string messageText)
        {
            Emails = emails;
            Subject = subject;
            MessageText = messageText;
            HiddenEmails = hiddenEmails;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}