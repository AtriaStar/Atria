namespace Atria.Models; 

public record Question(
    string Text,
    Answer[] Answers,
    DateTimeOffset CreationTime,
    User Creator
);
