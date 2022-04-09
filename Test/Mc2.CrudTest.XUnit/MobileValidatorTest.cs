using Mc2.CrudTest.Domain.Validators;
using Xunit;

namespace Mc2.CrudTest.XUnit
{

    public class MobileValidatorTest
    {
        [Theory]
        [InlineData("+989123456789", true)]
        [InlineData("+31612345678", true)]
        [InlineData("+982188776655", false)]
        [InlineData("+60327306464", false)]
        public void MobileValidatorTest_ExpectedResult(string phoneNumber, bool expectedResult)
        {
            bool testResult = MobileValidator.Validate(phoneNumber);
            Assert.True(testResult);
            Assert.Equal(expectedResult, testResult);
            //Assert.Equals(expectedResult, testResult);
        }
    }
    
}