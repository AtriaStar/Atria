using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models; 

public class Tag {
    [Key]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;
    public long UseCount => WebserviceEntries.Count;

    protected virtual ISet<WebserviceEntry> WebserviceEntries { get; set; } = new HashSet<WebserviceEntry>();

    private class Mapper : IEntityTypeConfiguration<Tag> {
        public void Configure(EntityTypeBuilder<Tag> builder) {
            builder
                .HasMany(x => x.WebserviceEntries)
                .WithMany(x => x.Tags);
        }
    }
}
