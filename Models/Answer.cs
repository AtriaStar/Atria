namespace Models;

public class Answer {
    public string Text { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; }
}
