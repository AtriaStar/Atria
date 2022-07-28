namespace Models;

// TODO: Notification ist abstract with getBaseMessage()
public class Notification {
    public string BaseMessage { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}
