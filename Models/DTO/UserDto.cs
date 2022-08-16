using System.ComponentModel.DataAnnotations;

namespace Models.DTO; 

public class UserDto {
    public string? Title { get; set; }
    [Required]
    public string FirstNames { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string? Biography { get; set; }
}