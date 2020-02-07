using System;
using NUnit.Framework;
using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    [TestFixture]
    //[Ignore("Тесты предназначеные для запуска только в ручном режиме")]
    public class TestRealEmailSending
    {
        private const string _host = "mail.tomsknipi.ru";
        private const int _port = 25;
        private const string _fromAddress = "InternalSoftwareSupport@tomsknipi.ru";
        private const string _fromDisplayName = "Тест сервиса отправки уведомлений";

        private NotificationSendingServiceImplementation _notificationService;
        private TestDateTimeProvider _dateTimeProvider;
        private TestSecurityLogger _securityLogger;
        private EmailClientProviderImplementation _emailClientProvider;
        
        [SetUp]
        public void SetUp()
        {
            var emailAddresValidator = new EmailAddressValidatorImplementation();
            var hiddenEmailListProvider = new HiddenEmailListProviderImplementation();
            _emailClientProvider = new EmailClientProviderImplementation(_host, _port, _fromAddress, _fromDisplayName);
            _securityLogger = new TestSecurityLogger();
            _dateTimeProvider = new TestDateTimeProvider();

            _notificationService = new NotificationSendingServiceImplementation(
                emailAddresValidator, 
                _emailClientProvider, 
                _securityLogger, 
                _dateTimeProvider,
                hiddenEmailListProvider);
        }
        
        [Test]
        public void TestRealSendingNotificationByEmail()
        {
            //Arrange
            const long callerId = 112;
            var validEmailList = new[] { "InternalSoftwareSupport@tomsknipi.ru" };
            const string expectedSubject = "[NotificationService] UnitTest";
            string expectedMessageText = $"Время: {DateTime.Now.ToString()}";
            const string expectedCategory = "Отправка уведомлений пользователям по почте";
            string expectedDescription = $"Пользователь с идентификатором {callerId} отправил уведомление по почте " +
                                         $"следующим адресатам: {string.Join(", ", validEmailList)}" +
                                         $"с темой {expectedSubject} и текстом сообщения {expectedMessageText}";
            var expectedActionDate = new DateTime(2001, 02, 03, 04, 05, 06);
            _dateTimeProvider.ValuesToReturn.Enqueue(expectedActionDate);

            //Act
            _notificationService.SendNotificationByEmail(callerId, validEmailList, expectedSubject, expectedMessageText);

            _securityLogger.DequeueAndCheckEntry(expectedActionDate, callerId, expectedCategory,
                expectedDescription, "Отправка уведомлений пользователям по почте");
            _securityLogger.AssertThatNoLogEntriesLeft("Проверка после отправки уведомлений пользователям по почте");

            _dateTimeProvider.AssertThatNoValuesLeft("Отправка уведомлений пользователям по почте");
        }
    }
}