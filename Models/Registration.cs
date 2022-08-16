using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Registration {
    
    [Required]
    public string FirstNames { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}