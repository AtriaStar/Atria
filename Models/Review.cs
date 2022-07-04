namespace Atria.Models; 

public record Review(
    string Title,
    string Description,
    StarCount StarCount,
    DateTimeOffset CreationTime,
    User Creator
);