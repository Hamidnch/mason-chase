using PhoneNumbers;

namespace Mc2.CrudTest.Domain.Validators;

public static class MobileValidator
{
    public static bool Validate(string phoneNumber)
    {
        PhoneNumberUtil? phoneNumberUtil = PhoneNumberUtil.GetInstance();
        PhoneNumber? p = phoneNumberUtil.Parse(phoneNumber, null);
        bool isMobile = false;
        bool isValidNumber =  phoneNumberUtil.IsValidNumber(p);
        PhoneNumberType numberType = phoneNumberUtil.GetNumberType(p);

        string phoneNumberType = numberType.ToString();

        if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
        {
            isMobile = true;
        }

        return isValidNumber && isMobile;

        //return p switch
        //{
        //    "+989123456789" => true,
        //    "+31612345678" => true,
        //    "+982188776655" => false,
        //    "+60327306464" => false,
        //    _ => false
        //};
    }
}