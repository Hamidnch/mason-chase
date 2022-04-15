using Mc2.CrudTest.Common.Helpers;

namespace Mc2.CrudTest.Common.Validators;

public static class MobileValidator
{
    public static bool Validate(string? phoneNumber)
    {
        return phoneNumber.IsValidMobileNumber();
    }
}