using Mc2.CrudTest.Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests
{
    public class MobileValidatorTestOld
    {
        [Theory]
        [InlineData("+989123456789", true)]
        [InlineData("+31612345678", true)]
        [InlineData("+982188776655", false)]
        [InlineData("+60327306464", false)]
        public void MobileValidatorTest_ExpectedResultOld(string phoneNumber, bool expectedResult)
        {
            bool testResult = MobileValidator.Validate(phoneNumber);

            Assert.Equals(expectedResult, testResult);
        }
    }
}