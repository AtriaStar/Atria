using System.ComponentModel.DataAnnotations;

namespace Models.DTO;

public class LoginDto {
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
