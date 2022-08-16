﻿using System.ComponentModel.DataAnnotations;

namespace Models; 

public class WebserviceEntry {
    [Key]
    public string Name { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public Uri Link { get; set; } = null!;
    public string FullDescription { get; set; } = null!;
    public Uri DocumentationLink { get; set; } = null!;
    public string Changelog { get; set; } = null!;
    public int ViewCount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public User ContactPerson { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;
    public ICollection<Collaborator> Collaborators { get; set; } = null!;
}
