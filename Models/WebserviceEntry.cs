namespace Atria.Models; 

public record WebserviceEntry(
    string Name,
    string ShortDescription,
    Uri Link,
    string FullDescription,
    Uri DocumentationLink, 
    string Changelog,
    int ViewCount,
    DateTimeOffset CreationDate,
    User ContactPerson,
    Question[] Questions,
    Tag[] Tags,
    Review[] Reviews,
    Dictionary<User, PermissionSet> Collaborators
);
