using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class ChangePassword
{

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    public string NewPassword { get; set; }

    [Required]
    [CompareProperty(nameof(NewPassword))]
    public string ConfirmPassword { get; set; }
}