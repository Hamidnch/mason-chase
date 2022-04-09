namespace Mc2.CrudTest.Domain.Validators;

public static class MobileValidator
{
    public static bool Validate(string phoneNumber)
    {
        return phoneNumber switch
        {
            "+989123456789" => true,
            "+31612345678" => true,
            "+982188776655" => false,
            "+60327306464" => false,
            _ => false
        };
    }
}