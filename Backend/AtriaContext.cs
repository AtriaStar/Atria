using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Models;

namespace Backend {
    public class AtriaContext : DbContext {
        public DbSet<User> Users => Set<User>();
        public DbSet<WSEDraft> Drafts => Set<WSEDraft>();
        public DbSet<WebserviceEntry> WebserviceEntries => Set<WebserviceEntry>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=Atria;Username=user;Password=password;Include Error Detail=true"); // TODO: Change
    }
}
