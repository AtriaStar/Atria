using Backend;
using Models;

await using var context = new AtriaContext();
Console.WriteLine(string.Join(", ", context.Users.Select(x => x.Email)));

WebserviceEntry entry = new WebserviceEntry {
    Name = "Google",
    ShortDescription = "Search things",
    FullDescription = "search many things",
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

WebserviceEntry entry2 = new WebserviceEntry {
    Name = "Wikipedia",
    ShortDescription = "Encyclopedia",
    FullDescription = "Big Encyclopedia",
    Link = new("https://www.wikipedia.de/"),
    ViewCount = 10,
    ContactPerson = new() {
        FirstNames = "Gerald",
        LastName = "of Rivia",
        Email = "geraldOfRivia@rivia.com",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.2",
    }
};

WebserviceEntry entry3 = new WebserviceEntry
{
    Name = "LeoOrg",
    ShortDescription = "Translator",
    FullDescription = "Die LEO GmbH bietet Ihnen die bekannten und beliebten Wörterbücher in den " +
    "Sprachen Englisch ⇔ Deutsch, Französisch ⇔ Deutsch, Spanisch ⇔ Deutsch, Italienisch ⇔ Deutsch, " +
    "Chinesisch ⇔ Deutsch, Russisch ⇔ Deutsch, Portugiesisch ⇔ Deutsch, Polnisch ⇔ Deutsch, Englisch ⇔ Spanisch, " +
    "Englisch ⇔ Französisch, Englisch ⇔ Russisch, sowieSpanisch ⇔ Portugiesisch. Des Weiteren können Sie in unseren " +
    "Foren unsere weltweite Community um Rat fragen, im Trainer Ihre Vokabelkenntnisse auffrischen oder mit unseren " +
    "Sprachkursen entspannt eine neue Sprache lernen oder verfeinern.",
    Link = new("https://www.leo.org/englisch-deutsch"),
    ViewCount = 10,
    ContactPerson = new()
    {
        FirstNames = "J. R. R.",
        LastName = "Tolkien",
        Email = "j.tolkien@middle-earth.com",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.3",
    }

};

WebserviceEntry entry4 = new WebserviceEntry
{
    Name = "Netflix",
    ShortDescription = "Serien und Filme online ansehen",
    FullDescription = "Netflix, Inc. (von Net, kurz für Internet und flicks als ein im Englischen " +
    "umgangssprachlicher Ausdruck für ‚Filme‘) ist ein US-amerikanisches Medienunternehmen, das sich " +
    "mit dem kostenpflichtigen Streaming und der Produktion von Filmen und Serien beschäftigt.",
    Link = new("https://www.netflix.com/"),
    ViewCount = 10000,
    ContactPerson = new()
    {
        FirstNames = "Gerald",
        LastName = "of Rivia",
        Email = "geraldOfRivia@rivia.com",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.2",
    }
};

WebserviceEntry entry5 = new WebserviceEntry
{
    Name = "NetflixAgain",
    ShortDescription = "Serien und Filme online ansehen",
    FullDescription = "Netflix, Inc. (von Net, kurz für Internet und flicks als ein im Englischen " +
    "umgangssprachlicher Ausdruck für ‚Filme‘) ist ein US-amerikanisches Medienunternehmen, das sich " +
    "mit dem kostenpflichtigen Streaming und der Produktion von Filmen und Serien beschäftigt.",
    Link = new("https://www.netflix.com/"),
    ViewCount = 10000,
    ContactPerson = new()
    {
        FirstNames = "Gerald",
        LastName = "of Rivia",
        Email = "geraldOfRivia@rivia.com",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.2",
    }
};


WebserviceEntry entry6 = new WebserviceEntry {
    Name = "Bing",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://www.bing.com/"),
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

WebserviceEntry entry7 = new WebserviceEntry
{
    Name = "ZBing",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://www.bing.com/"),
    ViewCount = 1,
    ContactPerson = new()
    {
        FirstNames = "Jane",
        LastName = "Smith",
        Email = "floppo@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.4",
    },
};

WebserviceEntry entry8 = new WebserviceEntry
{
    Name = "DuckDuckGo",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://duckduckgo.com/"),
    ViewCount = 1,
    ContactPerson = new()
    {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry9 = new WebserviceEntry
{
    Name = "Urban Dictionary",
    ShortDescription = "Dictonary",
    FullDescription = "Dictionary for english slang",
    Link = new("https://www.urbandictionary.com/"),
    ViewCount = 16523,
    ContactPerson = new()
    {
        FirstNames = "John",
        LastName = "Doe",
        Email = "floppi@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry10 = new WebserviceEntry
{
    Name = "NewDuckDuckGo",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://duckduckgo.com/"),
    ViewCount = 15675,
    ContactPerson = new()
    {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry11 = new WebserviceEntry
{
    Name = "TwoDuckDuckGo",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://duckduckgo.com/"),
    ViewCount = 1145145,
    ContactPerson = new()
    {
        FirstNames = "Johnny",
        LastName = "Smith",
        Email = "floppu@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry12 = new WebserviceEntry
{
    Name = "YouTube",
    ShortDescription = "Watch videos",
    FullDescription = "Watch videos provided by many users, pay if u don't want to only watch advertisment instead of videos.",
    Link = new("https://www.youtube.com/"),
    ViewCount = 1145,
    ContactPerson = new()
    {
        FirstNames = "Johanna",
        LastName = "Smith",
        Email = "floppe@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry13 = new WebserviceEntry
{
    Name = "Overleaf",
    ShortDescription = "Write LaTeX documents.",
    FullDescription = "Write documents for free using LaTeX. Work together with other people on one project at the same time.",
    Link = new("https://de.overleaf.com/"),
    ViewCount = 1114514,
    ContactPerson = new()
    {
        FirstNames = "Johanna",
        LastName = "Smith",
        Email = "floppe@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry14 = new WebserviceEntry
{
    Name = "TwoDuckDuckGo",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://duckduckgo.com/"),
    ViewCount = 32441,
    ContactPerson = new()
    {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry15 = new WebserviceEntry
{
    Name = "ThreeDuckDuckGo",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://duckduckgo.com/"),
    ViewCount = 1333,
    ContactPerson = new()
    {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

WebserviceEntry entry16 = new WebserviceEntry
{
    Name = "AnotherDuckDuckGo",
    ShortDescription = "Search things",
    FullDescription = "search many things",
    Link = new("https://duckduckgo.com/"),
    ViewCount = 33331,
    ContactPerson = new()
    {
        FirstNames = "John",
        LastName = "Smith",
        Email = "floppa@floppa.de",
        PasswordSalt = Array.Empty<byte>(),
        PasswordHash = Array.Empty<byte>(),
        SignUpIp = "127.0.0.1",
    },
};

User user1 = new User
{
    FirstNames = "John",
    LastName = "Smith",
    Email = "floppa@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user2 = new User
{
    FirstNames = "Gerald",
    LastName = "of Rivia",
    Email = "geraldOfRivia@rivia.com",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.2",
};

User user3 = new User
{
    FirstNames = "J. R. R.",
    LastName = "Tolkien",
    Email = "j.tolkien@middle-earth.com",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.3",
};

User user4 = new User
{
    FirstNames = "Jane",
    LastName = "Smith",
    Email = "floppo@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.4",
};

User user5 = new User
{
    FirstNames = "John",
    LastName = "Doe",
    Email = "floppi@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user6 = new User
{
    FirstNames = "Johnny",
    LastName = "Smith",
    Email = "floppu@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user7 = new User
{
    FirstNames = "Johanna",
    LastName = "Smith",
    Email = "floppe@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user8 = new User
{
    FirstNames = "Johann",
    LastName = "Smith",
    Email = "flopp@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user9 = new User
{
    FirstNames = "James",
    LastName = "Bond",
    Email = "jb@floppa.de",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user10 = new User
{
    FirstNames = "Michael",
    LastName = "Scofield",
    Email = "misco@break.com",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user11 = new User
{
    FirstNames = "Sherlock",
    LastName = "Holmes",
    Email = "sh@london.at",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user12 = new User
{
    Title = "Dr.",
    FirstNames = "John",
    LastName = "Watson",
    Email = "jw@london.at",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user13 = new User
{
    Title = "Dr.",
    FirstNames = "Meredith",
    LastName = "Grey",
    Email = "megr@seatlle.us",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user14 = new User
{
    FirstNames = "Tyrion",
    LastName = "Lannister",
    Email = "tlannister@lan.got",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user15 = new User
{
    FirstNames = "Cersei",
    LastName = "Lannister",
    Email = "clannister@lan.got",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

User user16 = new User
{
    FirstNames = "Jaime",
    LastName = "Lannister",
    Email = "jlannister@lan.got",
    PasswordSalt = Array.Empty<byte>(),
    PasswordHash = Array.Empty<byte>(),
    SignUpIp = "127.0.0.1",
};

await context.WebserviceEntries.AddAsync(entry);
await context.WebserviceEntries.AddAsync(entry2);
await context.WebserviceEntries.AddAsync(entry3);
await context.WebserviceEntries.AddAsync(entry4);
await context.WebserviceEntries.AddAsync(entry5);
await context.WebserviceEntries.AddAsync(entry6);
await context.WebserviceEntries.AddAsync(entry7);
await context.WebserviceEntries.AddAsync(entry8);
await context.WebserviceEntries.AddAsync(entry9);
await context.WebserviceEntries.AddAsync(entry10);
await context.WebserviceEntries.AddAsync(entry11);
await context.WebserviceEntries.AddAsync(entry12);
await context.WebserviceEntries.AddAsync(entry13);
await context.WebserviceEntries.AddAsync(entry14);
await context.WebserviceEntries.AddAsync(entry15);

await context.Users.AddAsync(user1);
await context.Users.AddAsync(user2);
await context.Users.AddAsync(user3);
await context.Users.AddAsync(user4);
await context.Users.AddAsync(user5);
await context.Users.AddAsync(user6);
await context.Users.AddAsync(user7);
await context.Users.AddAsync(user8);
await context.Users.AddAsync(user9);
await context.Users.AddAsync(user10);
await context.Users.AddAsync(user11);
await context.Users.AddAsync(user12);
await context.Users.AddAsync(user13);
await context.Users.AddAsync(user14);
await context.Users.AddAsync(user15);
await context.Users.AddAsync(user16);

await context.SaveChangesAsync();