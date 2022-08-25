using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend; 

public class AtriaContext : DbContext {
    public DbSet<User> Users => Set<User>();
    public DbSet<WSEDraft> Drafts => Set<WSEDraft>();
    public DbSet<WebserviceEntry> WebserviceEntries => Set<WebserviceEntry>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Session> Sessions => Set<Session>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Atria;Username=user;Password=password;Include Error Detail=true"); // TODO: Change

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Review>()
            .HasKey(x => new { x.WseId, x.Id });
        modelBuilder.Entity<Question>()
            .HasKey(x => new { x.WseId, x.Id });
        modelBuilder.Entity<Answer>()
            .HasKey(x => new { x.WseId, x.QuestionId, x.Id });
    }
}