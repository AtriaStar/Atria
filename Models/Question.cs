using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models; 

public class Question {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Text { get; set; } = null!;
    public ICollection<Answer> Answers { get; set; } = null!;
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; } = null!;
}
