using System.ComponentModel.DataAnnotations;

namespace Models.DTO; 

public class Registration {
    [Required]
    public string FirstNames { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    public string Password { get; set; } = null!;
    
    [CompareProperty(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}