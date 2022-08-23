using Backend;
using DatabaseMocker;
using Models;

await using var context = new AtriaContext();
Console.WriteLine(string.Join(", ", context.Users.Select(x => x.Email)));

var entityEntry = await UserMock.AddUser(context);
await context.SaveChangesAsync();
await WseMock.AddWse(context, entityEntry.Entity.Id);

await context.SaveChangesAsync();