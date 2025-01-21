using System.ComponentModel.DataAnnotations;

namespace WebApp.MVC.ValidationAttributes;

public class MyCustomAttribute : ValidationAttribute
{
    int[] _validValues;

    public MyCustomAttribute(int[] validValues)
    {
        _validValues = validValues;
    }
    public override bool IsValid(object? value)
    {
        //if (RequiresValidationContext)
        //{
            
        //}
        return base.IsValid(value);
    }
}