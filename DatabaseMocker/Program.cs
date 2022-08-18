using Backend;
using Models;

await using var context = new AtriaContext();
Console.WriteLine(string.Join(", ", context.Users.Select(x => x.Email)));

WebserviceEntry entry = new WebserviceEntry {
    Name = "Test",
    ShortDescription = "SD",
    FullDescription = "FUlld",
    Link = new Uri("https://google.de"),
    ViewCount = 1,
    ContactPerson = new User() {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "192"
    },
    Questions = new List<Question>(),
    Tags = new List<Tag>(),
    Reviews = new List<Review>(),
    Collaborators = new List<Collaborator>(),
    CreatedAt = DateTimeOffset.UtcNow
};

await context.WebserviceEntries.AddAsync(entry);


await context.SaveChangesAsync();