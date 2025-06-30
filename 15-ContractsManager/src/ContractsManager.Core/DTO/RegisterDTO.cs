using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "Name is required")]
    public string PersonName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain only digits")]
    [DataType(DataType.Password)]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}