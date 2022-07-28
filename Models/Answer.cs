namespace Models;

public record Answer(
    string Text,
    DateTimeOffset CreationTime,
    User Creator
);
