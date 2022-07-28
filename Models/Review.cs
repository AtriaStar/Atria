namespace Models; 

public class Review {
    public string Title { get; set; }
    public string Description { get; set; }
    public StarCount StarCount { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public User Creator  { get; set; }
}
