using NUnit.Framework;
using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    public class TestEmailAddressValidatorImplementation
    {
        private EmailAddressValidatorImplementation _emailAddressValidator;

        [SetUp]
        public void SetUp()
        {
            _emailAddressValidator = new EmailAddressValidatorImplementation();
        }

        [TestCase("test@test.ru", true)]
        [TestCase("testtest.ru", false)]
        [TestCase("@test.ru", false)]
        [TestCase("test@", false)]
        [TestCase("", false)]
        public void TestEmailAddressValidation(string email, bool result)
        {
            Assert.That(_emailAddressValidator.EmailAddressIsValid(email), Is.EqualTo(result), "");
        }
    }
}