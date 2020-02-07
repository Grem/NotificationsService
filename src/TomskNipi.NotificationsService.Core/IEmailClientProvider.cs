using System;

namespace TomskNipi.NotificationsService.Core
{
    /// <summary>
    /// Интерфейс для получения списка адресов для добавления в скрытую копию
    /// </summary>
    public interface IEmailClientProvider : IDisposable
    {
        /// <summary>
        /// Отправляет уведомление на список email адресов.
        /// </summary>
        /// <param name="emails">Список адресов, на которые необходимо отправить уведомление.</param>
        /// <param name="hiddenEmails">Список адресов</param>
        /// <param name="subject">Тема уведомления</param>
        /// <param name="messageText">Текст уведомления</param>
        void Send(string[] emails, string[] hiddenEmails, string subject, string messageText);
    }
}