using Backend;
using Models;

await using var context = new AtriaContext();
Console.WriteLine(string.Join(", ", context.Users.Select(x => x.Email)));

WebserviceEntry entry = new WebserviceEntry {
    Name = "Test",
    ShortDescription = "SD",
    FullDescription = "FUlld",
    Link = new("https://google.de"),
    ViewCount = 1,
    ContactPerson = new() {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

await context.WebserviceEntries.AddAsync(entry);


await context.SaveChangesAsync();
