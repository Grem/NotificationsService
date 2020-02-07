using System;

namespace TomskNipi.NotificationsService.Core
{
    /// Grebenkovma TECHDEBT Скопировано из проекта UserService
    /// <summary>
    /// Поставщик даты и времени для сервиса управления пользователями.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Возвращает текущую дату и время.
        /// </summary>
        /// <returns>Текущая дата и время в UTC.</returns>
        DateTime GetCurrentDateTime();
    }
}