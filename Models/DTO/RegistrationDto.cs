using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.DTO; 

public class RegistrationDto {
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
    [JsonIgnore]
    public string ConfirmPassword { get; set; } = null!;
}