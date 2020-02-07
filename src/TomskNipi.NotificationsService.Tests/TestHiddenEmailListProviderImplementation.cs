using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    /// <summary>
    /// Тестовая реализация провайдера для получения 
    /// </summary>
    public class TestHiddenEmailListProviderImplementation : IHiddenEmailListProvider
    {
        public string[] hiddenEmailsToReturn;

        public string[] GetEmailList()
        {
            return hiddenEmailsToReturn;
        }
    }
}