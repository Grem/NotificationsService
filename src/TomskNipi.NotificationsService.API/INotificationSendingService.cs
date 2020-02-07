using System;

namespace TomskNipi.NotificationsService.API
{
    /// <summary>
    /// Интерфейс для рассылки уведомлений.
    /// </summary>
    public interface INotificationSendingService : IDisposable
    {
        /// <summary>
        /// Отправляет уведолмление на список e-mail адресов
        /// </summary>
        /// <param name="callerId">Идентификатор пользователя вызвавшего метод</param>
        /// <param name="emailList">Список e-mail адресов</param>
        /// <param name="subject">Тема уведомления</param>
        /// <param name="messageText">Текст уведомления</param>
        void SendNotificationByEmail(long callerId, string[] emailList, string subject, string messageText);

        /// <summary>
        /// Отправляет уведомления списку пользователей
        /// </summary>
        /// <param name="callerId">Идентификатор пользователя вызвавшего метод</param>
        /// <param name="userIdList">Список идентификаторов пользователей</param>
        /// <param name="subject">Тема уведомления</param>
        /// <param name="messageText">Текст уведомления</param>
        void SendNotificationByUserId(long callerId, long[] userIdList, string subject, string messageText);
    }
}