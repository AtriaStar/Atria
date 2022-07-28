namespace Models; 

public class Question {
    public string Text { get; set; }
    public Answer[] Answers { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public User Creator { get; set; }
}
