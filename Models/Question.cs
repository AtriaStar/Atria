using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Question {
    [Key]
    public ulong Snowflake { get; set; }
    public string Text { get; set; }
    public Answer[] Answers { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; }
}
