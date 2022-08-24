using Backend;
using DatabaseMocker;

await using var context = new AtriaContext();

context.WebserviceEntries.RemoveRange(context.WebserviceEntries);
context.Users.RemoveRange(context.Users);
await context.SaveChangesAsync();

await context.Users.AddRangeAsync(Enumerable.Range(0, 100)
    .Select(_ => UserMocker.GenerateUser())
    .DistinctBy(x => x.Email));
await context.SaveChangesAsync();

await WseMocker.AddWse(context, context.Users.RandomElement().Id);

await context.SaveChangesAsync();
