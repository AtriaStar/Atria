using Backend;
using Backend.AspPlugins;
using DatabaseMocker;
using Microsoft.Extensions.Configuration;
using Models;

Console.WriteLine(RootDirectory.GetTest());
Console.WriteLine(Directory.GetParent(RootDirectory.GetTest()));
Console.WriteLine(Directory.GetParent(RootDirectory.GetTest().Replace('\\', '/')));
// TODO: Works correctly here??

await using var context = new AtriaContext(new ConfigurationBuilder()
    .AddStandardSources("Mocker").Build()
    .CreateAtriaOptions<BackendSettings>());

context.WebserviceEntries.RemoveRange(context.WebserviceEntries);
context.Users.RemoveRange(context.Users);
await context.SaveChangesAsync();

await context.Users.AddRangeAsync(Enumerable.Range(0, 100)
    .Select(_ => UserMocker.GenerateUser())
    .DistinctBy(x => x.Email));
await context.SaveChangesAsync();

await UserMocker.AddUser(context);

await WseMocker.AddWse(context, context.Users.RandomElement().Id);

await context.SaveChangesAsync();
