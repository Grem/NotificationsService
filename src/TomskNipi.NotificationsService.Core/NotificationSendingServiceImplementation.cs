using System;
using System.Net;
using System.Net.Mail;
using NLog;
using TomskNipi.NotificationsService.API;

namespace TomskNipi.NotificationsService.Core
{
    /// <inheritdoc />
    public class NotificationSendingServiceImplementation : INotificationSendingService
    {
        private const string category = "Отправка уведомлений пользователям по почте";

        private readonly IEmailAddressValidator _emailAddressValidator;
        private readonly IEmailClientProvider _emailClientProvider;
        private readonly ISecurityLogger _securityLogger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHiddenEmailListProvider _hiddenEmailListProvider;

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public NotificationSendingServiceImplementation(
            IEmailAddressValidator emailAddressValidator,
            IEmailClientProvider emailClientProvider,
            ISecurityLogger securityLogger,
            IDateTimeProvider dateTimeProvider,
            IHiddenEmailListProvider hiddenEmailListProvider)
        {
            _emailAddressValidator = emailAddressValidator;
            _emailClientProvider = emailClientProvider;
            _securityLogger = securityLogger;
            _dateTimeProvider = dateTimeProvider;
            _hiddenEmailListProvider = hiddenEmailListProvider;
        }

        /// <inheritdoc />
        public void SendNotificationByEmail(long callerId, string[] emailList, string subject, string messageText)
        {
            Logger.Trace($"SendNotificationByEmail(): выполнение начато");

            foreach (var email in emailList)
            {
                if (!_emailAddressValidator.EmailAddressIsValid(email))
                    throw new ArgumentException();
            }

            var hiddenEmail = _hiddenEmailListProvider.GetEmailList();
            _emailClientProvider.Send(emailList, hiddenEmail, subject, messageText);

            var description = $"Пользователь с идентификатором {callerId} отправил уведомление по почте " +
                              $"следующим адресатам: {string.Join(", ", emailList)}" +
                              $"с темой {subject} и текстом сообщения {messageText}";
            LogSecurityAction(callerId, category, description);

            Logger.Trace($"SendNotificationByEmail(): выполнение завершено.");
        }

        /// Grebenkovma TECHDEBT Данный метод скопирован из проекта UserServices
        private void LogSecurityAction(long callerId, string category, string description)
        {
            var currentDate = _dateTimeProvider.GetCurrentDateTime();
            _securityLogger.LogAction(currentDate, callerId, category, description);
        }

        public void SendNotificationByUserId(long callerId, long[] userIdList, string subject, string messageText)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}