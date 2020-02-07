using System;
using NUnit.Framework;

namespace TomskNipi.NotificationsService.Tests
{
    /// Grebenkovma TECHDEBT Данный класс скопирован из проекта UserServices
    /// <summary>
    /// Запись из тестового журнала действий пользователей.
    /// </summary>
    public class SecurityLogEntry
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="actionDate">Дата действия.</param>
        /// <param name="userId">Идентификатор пользователя, выполнившего действие.</param>
        /// <param name="category">Категория действия.</param>
        /// <param name="description">Описание действия.</param>
        public SecurityLogEntry(DateTime actionDate, long userId, string category, string description)
        {
            ActionDate = actionDate;
            UserId = userId;
            Category = category;
            Description = description;
        }


        /// <summary>
        /// Дата выполнения действия.
        /// </summary>
        public DateTime ActionDate { get; }

        /// <summary>
        /// Идентификатор пользователя, выполнившего действие.
        /// </summary>
        public long UserId { get; }

        /// <summary>
        /// Категория выполненного действия.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Описание действия.
        /// </summary>
        public string Description { get; }


        public void AssertThatPropertiesAreEqualTo(DateTime expectedActionDate, long expectedUserId,
            string expectedCategory, string expectedDescription, string assertionDescription)
        {
            Assert.That(ActionDate, Is.EqualTo(expectedActionDate),
                $"{assertionDescription}: дата выполнения действия не совпадает с ожидаемой");
            Assert.That(UserId, Is.EqualTo(expectedUserId),
                $"{assertionDescription}: идентификатор пользователя не совпадает с ожидаемым");
            Assert.That(Category, Is.EqualTo(expectedCategory),
                $"{assertionDescription}: категория действия не совпадает с ожидаемой");
            Assert.That(Description, Is.EqualTo(expectedDescription),
                $"{assertionDescription}: описание действия не совпадает с ожидаемым");
        }
    }
}
