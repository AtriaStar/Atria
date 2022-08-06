using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Question {
    [Key]
    public ulong Id { get; set; }
    public string Text { get; set; } = null!;
    public ICollection<Answer> Answers { get; set; } = null!;
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; } = null!;
}
