namespace Models;

// TODO: Notification ist abstract with getBaseMessage()
public record Notification(
    string baseMessage,
    DateTimeOffset creationTime
);
