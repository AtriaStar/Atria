namespace Atria.Models;

public record WSEDraft(
    string Name,
    string ShortDescription,
    Uri Link,
    string FullDescription,
    Uri DocumentationLink,
    string Changelog,
    DateTimeOffset CreationDate
);
