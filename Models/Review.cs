using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Review {
    [Key]
    public ulong Snowflake { get; set; }
    public string Title { get; init; }
    public string Description { get; init; }
    public StarCount StarCount { get; init; }
    public DateTimeOffset CreationTime { get; init; }
    public User Creator { get; init; }
}
