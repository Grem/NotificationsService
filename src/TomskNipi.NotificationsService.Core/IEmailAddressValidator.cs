namespace TomskNipi.NotificationsService.Core
{
    /// <summary>
    /// Провайдер осуществляющий валидацию e-mail адреса
    /// </summary>
    public interface IEmailAddressValidator
    {
        /// <summary>
        /// Проверяет валидность e-mail адреса
        /// </summary>
        /// <param name="email">Е-mail адрес</param>
        /// <returns></returns>
        bool EmailAddressIsValid(string email);
    }
}