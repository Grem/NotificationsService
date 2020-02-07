using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using NLog;
using TomskNipi.NotificationsService.API;

namespace TomskNipi.NotificationsService.Core
{
    public class EmailClientProviderImplementation : IEmailClientProvider
    {
        private readonly string _fromAddress;
        private readonly string _fromDisplayName;
        private SmtpClient _smtp;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public EmailClientProviderImplementation(string host, int port, string fromAddress, string fromDisplayName)
        {
            _fromAddress = fromAddress;
            _fromDisplayName = fromDisplayName;
            _smtp = new SmtpClient(host, port);
        }

        private MailMessage GetMailMessage(string fromAddress, string fromDisplayName, string[] emails, string[] hiddenEmails, string subject, string message)
        {
            MailAddress from = new MailAddress(fromAddress, fromDisplayName);

            var mailMessage = new MailMessage { From = from };

            foreach (var email in emails)
            {
                mailMessage.To.Add(new MailAddress(email));
            }

            if (hiddenEmails.Length != 0)
            {
                foreach (var hoddenEmail in hiddenEmails)
                {
                    mailMessage.Bcc.Add(new MailAddress(hoddenEmail));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }

        public void Send(string[] emails, string[] hiddenEmails, string subject, string messageText)
        {
            try
            {
                Logger.Debug($"Формирование сообщения следующим адресатам: {string.Join(", ", emails)}, " +
                             $"с темой {subject} и текстом сообщения {messageText}");
                var mailMessage = GetMailMessage(_fromAddress, _fromDisplayName, emails, hiddenEmails, subject, messageText);

                Logger.Debug($"Отправка сообщения следующим адресатам: {string.Join(", ", emails)}, " +
                             $"с темой {subject} и текстом сообщения {messageText}");
                _smtp.Send(mailMessage);
            }
            catch (Exception e)
            {
                Logger.Warn(e, "Попытка отправки сообщения с темой {subject} завершилась исключением.");
                throw new NotificationsServiceException($"Попытка отправки сообщения с темой {subject} завершилась исключением.", e);
            }
        }

        public void Dispose()
        {
            _smtp?.Dispose();
        }
    }
}