namespace Models;

public class User {
    public string Title { get; set; }
    public string FirstNames { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Uri ProfilePicture { get; set; }
    public string Biography { get; set; }
    public string PasswordHash { get; set; }
    public WebserviceEntry[] Bookmarks { get; set; }
}
