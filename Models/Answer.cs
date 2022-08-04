using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Answer {
    [Key]
    public ulong Snowflake { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; }
}
