using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.AspPlugins;

public class AtriaContext : DbContext {
    public DbSet<User> Users => Set<User>();
    public DbSet<WebserviceEntry> WebserviceEntries => Set<WebserviceEntry>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<ResetToken> ResetTokens => Set<ResetToken>();

    private readonly string _connectingString;
    private readonly bool _detailedErrors;

    public AtriaContext(BackendSettings opt) {
        _connectingString = opt.DatabaseString;
        _detailedErrors = opt.DatabaseDetailedErrors;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(_connectingString)
            .UseSnakeCaseNamingConvention();
        if (_detailedErrors) {
            optionsBuilder.EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebserviceEntry).Assembly);

        modelBuilder.Entity<Review>()
            .HasKey(x => new { x.WseId, x.Id });
        modelBuilder.Entity<Question>()
            .HasKey(x => new { x.WseId, x.Id });
        modelBuilder.Entity<Answer>()
            .HasKey(x => new { x.WseId, x.QuestionId, x.Id });
        modelBuilder.Entity<Collaborator>()
            .HasKey(x => new { x.WseId, x.UserId });

        modelBuilder.Entity<WebserviceEntry>()
            .HasOne(x => x.ContactPerson)
            .WithMany()
            .HasForeignKey(x => x.ContactPersonId);

        modelBuilder.Entity<WebserviceEntry>()
            .HasMany(x => x.Tags)
            .WithMany(x => x.WebserviceEntries);

        modelBuilder.Entity<WebserviceEntry>()
            .Navigation(x => x.Tags)
            .AutoInclude();
    }
}
