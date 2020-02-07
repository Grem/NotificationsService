using System;
using NUnit.Framework;
using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    [TestFixture]
    public class NotificationSendingServiceImplementationTests
    {
        private NotificationSendingServiceImplementation _notificationService;
        private TestDateTimeProvider _dateTimeProvider;
        private TestSecurityLogger _securityLogger;
        private TestEmailClientProviderImplementation _emailClientProvider;
        private TestHiddenEmailListProviderImplementation _hiddeEmailListProvider;

        [SetUp]
        public void SetUp()
        {
            var emailAddresValidator = new EmailAddressValidatorImplementation();
            _hiddeEmailListProvider = new TestHiddenEmailListProviderImplementation();
            _emailClientProvider = new TestEmailClientProviderImplementation();
            _securityLogger = new TestSecurityLogger();
            _dateTimeProvider = new TestDateTimeProvider();

            _notificationService = new NotificationSendingServiceImplementation(
                emailAddresValidator, 
                _emailClientProvider, 
                _securityLogger, 
                _dateTimeProvider, 
                _hiddeEmailListProvider);
        }

        
        [Test]
        public void TestSendingNotificationByEmail()
        {
            //Arrange
            const long callerId = 112;
            var validEmailList = new [] {"testuser@email.com"};
            const string expectedSubject = "testSubject";
            const string expectedMessageText = "expected message text!";
            var expectedActionDate = new DateTime(2001, 02, 03, 04, 05, 06);
            _dateTimeProvider.ValuesToReturn.Enqueue(expectedActionDate);

            const string expectedCategory = "Отправка уведомлений пользователям по почте";
            string expectedDescription = $"Пользователь с идентификатором {callerId} отправил уведомление по почте " +
                                         $"следующим адресатам: {string.Join(", ", validEmailList)}" +
                                         $"с темой {expectedSubject} и текстом сообщения {expectedMessageText}";
            const string expectedHiddenEmail = "testHiddenUser@email.ru";

            _hiddeEmailListProvider.hiddenEmailsToReturn = new string[] { expectedHiddenEmail };

            //Act
            _notificationService.SendNotificationByEmail(callerId, validEmailList, expectedSubject, expectedMessageText);

            //Assert
            Assert.That(_emailClientProvider.Subject,Is.EqualTo(expectedSubject),"Темы сообщений отличаются");
            Assert.That(_emailClientProvider.MessageText, Is.EqualTo(expectedMessageText),"Тексты сообщений отличаются");
            Assert.That(_emailClientProvider.Emails.Length,Is.EqualTo(validEmailList.Length),"Количество элементов в массивах не одинаково");
            Assert.That(_emailClientProvider.HiddenEmails.Length,Is.EqualTo(1), "Количество адресов для скрытой копии больше 1-го");
            Assert.That(_emailClientProvider.HiddenEmails[0], Is.EqualTo(expectedHiddenEmail), "Адреса скрытой копии отличаются");

            for (var i = 0; i < _emailClientProvider.Emails.Length; i++)
            {
                Assert.That(_emailClientProvider.Emails[i], Is.EqualTo(validEmailList[i]), $"Элемент с индексом {i} не совпадает с ожидаемым");
            }
            _securityLogger.DequeueAndCheckEntry(expectedActionDate, callerId, expectedCategory,
                expectedDescription, "Отправка уведомлений пользователям по почте");
            _securityLogger.AssertThatNoLogEntriesLeft("Проверка после отправки уведомлений пользователям по почте");

            _dateTimeProvider.AssertThatNoValuesLeft("Отправка уведомлений пользователям по почте");
        }
    }
}