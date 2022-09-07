using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.DTO;

public class ChangePasswordDto {
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    public string NewPassword { get; set; } = null!;

    [Required]
    [CompareProperty(nameof(NewPassword))]
    [JsonIgnore]
    public string ConfirmPassword { get; set; } = null!;
}
