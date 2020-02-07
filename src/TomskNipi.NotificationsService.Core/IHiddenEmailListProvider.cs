namespace TomskNipi.NotificationsService.Core
{
    /// <summary>
    /// Интерфейс провайдера для получения данных из конфигурационного файла сервиса
    /// </summary>
    public interface IHiddenEmailListProvider
    {
        /// <summary>
        /// Получает список адресов для скрытой копии
        /// </summary>
        /// <returns></returns>
        string[] GetEmailList();
    }
}