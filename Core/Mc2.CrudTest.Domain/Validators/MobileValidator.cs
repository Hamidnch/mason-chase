using Mc2.CrudTest.Common.Helpers;

namespace Mc2.CrudTest.Domain.Validators;

public static class MobileValidator
{
    public static bool Validate(string phoneNumber)
    {
        return CommonHelper.IsValidMobileNumber(phoneNumber: phoneNumber);
    }
}