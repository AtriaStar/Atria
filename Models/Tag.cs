using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Tag {
    [Key]
    public string Name { get; init; }
    public DateTimeOffset CreationTime { get; init; }
    public uint UseCount { get; init; }
}
