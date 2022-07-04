namespace Atria.Models; 

public record Tag(
    string Name,
    DateTimeOffset CreationTime,
    uint UseCount
);
