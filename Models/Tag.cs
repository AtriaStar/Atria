namespace Models; 

public class Tag {
    public string Name { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public uint UseCount { get; set; }
}
