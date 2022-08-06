using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models; 

public class Review {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public StarCount StarCount { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; } = null!;
}
