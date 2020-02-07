using System.Text.RegularExpressions;

namespace TomskNipi.NotificationsService.Core
{
    public class EmailAddressValidatorImplementation : IEmailAddressValidator
    {
        public bool EmailAddressIsValid(string email)
        {
            var pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            var isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);

            return isMatch.Success;
        }
    }
}