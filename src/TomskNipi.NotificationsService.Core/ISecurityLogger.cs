using System;

namespace TomskNipi.NotificationsService.Core
{
    /// Grebenkovma TECHDEBT: Этот интерфейс был скопирован из проекта UserServices
    /// <summary>
    /// Логгер, предназначенный для журналирования различных действий пользователя.
    /// Полученный журнал будет передаваться для анализа службе безопасности.
    /// </summary>
    public interface ISecurityLogger
    {
        /// <summary>
        /// Сохраняет запись о действии с указанными параметрами.
        /// </summary>
        /// <param name="actionTime">Дата и время выполнения действия.</param>
        /// <param name="userId">Идентификатор пользователя, выполнившего действие.</param>
        /// <param name="category">Общая категория выполненного действия (например, "Поиск пользователя", 
        /// "Удаление группы" и т.п.).</param>
        /// <param name="description">Описание выполненного действия.</param>
        void LogAction(DateTime actionTime, long userId, string category, string description);
    }
}
