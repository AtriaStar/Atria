using Backend;
using Backend.AspPlugins;
using DatabaseMocker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UnitTests;

public class DatabaseTests {
    private readonly AtriaContext _context;

    public DatabaseTests() {
        _context = new(new ConfigurationBuilder()
            .AddStandardSources("Tests").Build()
            .CreateAtriaOptions<BackendSettings>());

        foreach (var property in typeof(AtriaContext).GetProperties()
                     .Where(x => x.PropertyType.IsGenericType
                                 && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))) {
            var set = (dynamic)property.GetValue(_context)!;
            set.RemoveRange(set);
        }

        _context.SaveChanges();
    }

    [Fact]
    public async Task TestDatabase() {
        await _context.Users.AddRangeAsync(Enumerable.Range(0, 150)
            .Select(_ => UserMocker.GenerateUser())
            .DistinctBy(x => x.Email)
            .Take(100));
        await _context.SaveChangesAsync();
        Assert.Equal(100, _context.Users.Count());
    }
}
