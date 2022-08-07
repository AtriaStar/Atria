using System.ComponentModel.DataAnnotations;

namespace Models; 

public class Tag {
    [Key]
    public string Name { get; set; } = null!;
    public DateTimeOffset CreationTime { get; set; }
    public uint UseCount { get; set; }
}
