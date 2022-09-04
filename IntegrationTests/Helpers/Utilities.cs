using Backend.AspPlugins;
using DatabaseMocker;

namespace IntegrationTests.Helpers
{
    public static class Utilities
    {

        public static void InitializeDbForTests(AtriaContext db)
        {
            ClearDb(db);
            db.Users.AddRangeAsync(Enumerable.Range(0, 100)
            .Select(_ => UserMocker.GenerateUser())
            .DistinctBy(x => x.Email));
            db.SaveChanges();
            WseMocker.AddWse(db, db.Users.RandomElement().Id);
            db.SaveChanges();
        }

        public static void ClearDb(AtriaContext db)
        {
            db.WebserviceEntries.RemoveRange(db.WebserviceEntries);
            db.Users.RemoveRange(db.Users);
            db.Drafts.RemoveRange(db.Drafts);
            db.Answers.RemoveRange(db.Answers);
            db.Questions.RemoveRange(db.Questions);
            db.Reviews.RemoveRange(db.Reviews);
            db.SaveChanges();
        }
    }
}
