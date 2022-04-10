using Mc2.CrudTest.Domain.Validators;
using Xunit;

namespace Mc2.CrudTest.XUnit
{
    public class EmailValidatorTest
    {
        [Theory]
        [InlineData("hamidnch2007@gmail.com", true)]
        [InlineData("xyz@gm.com", false)]
        [InlineData("hamidnch2001@yahoo.com", true)]
        [InlineData("hamidnch@ms.c", false)]
        public void EmailValidatorTest_ExpectedResult(string email, bool expectedResult)
        {
            bool testResult = EmailValidator.Validate(email);
            Assert.Equal(expectedResult, testResult);
        }
    }
}
