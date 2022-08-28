using Backend.AspPlugins;
using DatabaseMocker;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class DatabaseTests {
    public DatabaseTests() {
        using var db = new AtriaContext();
        foreach (var property in typeof(AtriaContext).GetProperties()
                     .Where(x => x.PropertyType.IsGenericType
                                 && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))) {
            var set = (dynamic)property.GetValue(db)!;
            set.RemoveRange(set);
        }

        db.SaveChanges();
    }

    [Fact]
    public async Task TestDatabase() {
        await using var db = new AtriaContext();
        await db.Users.AddRangeAsync(Enumerable.Range(0, 150)
            .Select(_ => UserMocker.GenerateUser())
            .DistinctBy(x => x.Email)
            .Take(100));
        await db.SaveChangesAsync();
        Assert.Equal(100, db.Users.Count());
    }
}
