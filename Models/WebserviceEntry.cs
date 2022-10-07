using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace Models;

public class WebserviceEntry {
    private string? _documentationLink;
    private string? _apiCheckUrl;

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ShortDescription { get; set; } = null!;

    [Required]
    [Url]
    public string Link { get; set; } = null!;
    public string? FullDescription { get; set; }

    [Url]
    public string? ApiCheckUrl {
        get => _apiCheckUrl;
        set => _apiCheckUrl = string.IsNullOrEmpty(value) ? null : value;
    }
    [JsonIgnore]
    public virtual ICollection<ApiCheck> ApiCheckHistory { get; set; } = new List<ApiCheck>();
    public HttpStatusCode? LatestCheckStatus { get; set; }

    [Url]
    public string? DocumentationLink {
        get => _documentationLink;
        set => _documentationLink = string.IsNullOrEmpty(value) ? null : value;
    }

    public string? Documentation { get; set; }
    public string? ChangeLog { get; set; }

    public long ViewCount { get; set; }

    public long ContactPersonId { get; set; }
    [JsonIgnore]
    public virtual User ContactPerson { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [MaxLength(20)]
    public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();

    [JsonIgnore]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    [JsonIgnore, MaxLength(20)]
    public virtual ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    protected virtual ICollection<User> Bookmarkees { get; set; } = new List<User>();

    private class Mapper : IEntityTypeConfiguration<WebserviceEntry> {
        public void Configure(EntityTypeBuilder<WebserviceEntry> builder) {
            builder
                .HasMany(x => x.Bookmarkees)
                .WithMany(x => x.Bookmarks)
                .UsingEntity(x => x.ToTable("bookmarks"));
        }
    }
}
