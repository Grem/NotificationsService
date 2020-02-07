using System;
using System.Runtime.Serialization;

namespace TomskNipi.NotificationsService.API
{
    /// <summary>
    /// Класс, представляющий ошибки, связанные с работой NotificationServiceImplementation
    /// </summary>
    [Serializable]
    public class NotificationsServiceException : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public NotificationsServiceException()
        {
        }

        /// <summary>
        /// Конструктор исключения с заданным сообщением.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public NotificationsServiceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор исключения с заданным сообщением и указанным вложенным исключением.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="inner">Исключение</param>
        public NotificationsServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotificationsServiceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
