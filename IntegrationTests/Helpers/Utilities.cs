using Backend.AspPlugins;
using Backend.Services;
using DatabaseMocker;
using Microsoft.EntityFrameworkCore;
using Models;

namespace IntegrationTests.Helpers; 

public static class Utilities {
    public static void InitializeDbForTests(AtriaContext db) {
        ClearDb(db);
        db.Users.AddRangeAsync(Enumerable.Range(0, 100)
            .Select(_ => UserMocker.GenerateUser())
            .DistinctBy(x => x.Email));
        db.SaveChanges();
        var _ = WseMocker.AddWse(db, db.Users.RandomElement().Id).Result;
        db.SaveChanges();
    }

    public static void ClearDb(AtriaContext db) {
        foreach (var property in typeof(AtriaContext).GetProperties()
                     .Where(x => x.PropertyType.IsGenericType
                                 && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))) {
            var set = (dynamic)property.GetValue(db)!;
            set.RemoveRange(set);
        }

        db.SaveChanges();
    }

    public const string MASTER_PASSWORD = "12345";
    public const string MASTER_TOKEN = "12345";

    public static readonly byte[] MASTER_SALT = HashingService.GenerateSalt();
    public static readonly byte[] MASTER_HASHED_PASSWORD = HashingService.Hash(MASTER_PASSWORD, MASTER_SALT);

    public static async Task<Session> GetAuthenticatedUser(AtriaContext db) {
        var session = db.Sessions.Include(x => x.User).FirstOrDefault();
        if (session == null) {
            var user = await db.Users.FirstAsync();
            session = new() {
                User = user,
                Ip = "127.0.0.1",
                Token = MASTER_TOKEN,
                UserAgent = "Goggle Chrum",
            };
            await db.Sessions.AddAsync(session);
            await db.SaveChangesAsync();
        }

        return session;
    }
}