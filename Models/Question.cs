using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models; 

public class Question {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Key]
    public long WseId { get; set; }
    public WebserviceEntry Wse { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; } = null!;
}
