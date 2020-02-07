using System;
using System.Collections.Generic;
using NUnit.Framework;
using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    /// Grebenkovma TECHDEBT Данный класс скопирован из проекта UserServices
    /// <summary>
    /// Тестовая реализация журнала действий пользователей для СБ.
    /// </summary>
    public class TestSecurityLogger : ISecurityLogger
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public TestSecurityLogger()
        {
            LoggedEntries = new Queue<SecurityLogEntry>();
        }

        /// <summary>
        /// Сообщения, накопленные этим экземпляром логгера.
        /// </summary>
        public Queue<SecurityLogEntry> LoggedEntries { get; }


        /// <inheritdoc />
        public void LogAction(DateTime actionTime, long userId, string category, string description)
        {
            var entry = new SecurityLogEntry(actionTime, userId, category, description);
            LoggedEntries.Enqueue(entry);
        }


        /// <summary>
        /// Извлекает из очереди накопленных сообщений одно, самое раннее, сообщение и сверяет его параметры
        /// с требуемыми значениями.
        /// </summary>
        /// <param name="actionDate">Ожидаемая дата выполнения действия.</param>
        /// <param name="userId">Ожидаемый идентификатор пользователя.</param>
        /// <param name="category">Ожидаемая категория сообщения.</param>
        /// <param name="description">Ожидаемое описание действия.</param>
        /// <param name="assertionDescription">Краткое описание сообщения (для вывода в сообщениях о провале сравнений).</param>
        public void DequeueAndCheckEntry(DateTime actionDate, long userId, string category,
            string description, string assertionDescription)
        {
            Assert.That(LoggedEntries, Has.Count.GreaterThanOrEqualTo(1),
                $"{assertionDescription}: метод LogAction() вызывался меньше раз, чем ожидалось тестом");

            var queuedEntry = LoggedEntries.Dequeue();
            queuedEntry.AssertThatPropertiesAreEqualTo(actionDate, userId, category, description, assertionDescription);
        }

        /// <summary>
        /// Проверяет, все ли накопленные сообщения были проверены. Выполнение этого метода пройдёт успешно,
        /// если в очереди накопленных сообщений не будет ни одного сообщения.
        /// </summary>
        /// <param name="assertionDescription"></param>
        public void AssertThatNoLogEntriesLeft(string assertionDescription)
        {
            Assert.That(LoggedEntries, Is.Empty,
                $"{assertionDescription}: журнал содержит неожиданные сообщения");
        }
    }
}
