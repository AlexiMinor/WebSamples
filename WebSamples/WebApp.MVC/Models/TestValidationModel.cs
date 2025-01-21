using System.ComponentModel.DataAnnotations;

namespace WebApp.MVC.Models;

public class TestValidationModel
{
    [Required(ErrorMessage = "The Id field is required.")]
    public int Id { get; set; }

    [RegularExpression("\r\n^[a-z0-9]+(?!.*(?:\\+{2,}|\\-{2,}|\\.{2,}))(?:[\\.+\\-]{0,1}[a-z0-9])*@gmail\\.com$", ErrorMessage = "Your email is not valid")]
    public string Email { get; set; }

    [Required]
    //[MinLength(3)]
    [StringLength(256, MinimumLength = 2)]
    public string Title { get; set; }

    [Range(-5,5)]
    //[Url]
    //[Phone]
    //[CreditCard]
    public int Rate { get; set; }
}