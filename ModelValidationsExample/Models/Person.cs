using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models;

public class Person
{
    [Required(ErrorMessage = "{0} can't be empty or null")]
    [Display(Name = "Person Name")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
    public string? Name { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }

    [Range(0, 999.99, ErrorMessage = "{0} must be between {1} and {2}")]
    public double? Price { get; set; }

    public override string ToString()
    {
        return $"Person: {Name}, {Email}, {Phone}, {Password}, {ConfirmPassword}, {Price}";
    }
}