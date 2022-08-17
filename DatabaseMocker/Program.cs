using Backend;

await using var context = new AtriaContext();
Console.WriteLine(string.Join(", ", context.Users.Select(x => x.Email)));
