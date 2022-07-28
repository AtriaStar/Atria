namespace Models;

public record User(
    string Title,
    string FirstNames,
    string LastName,
    string Email,
    Uri ProfilePicture,
    string Biography,
    string PasswordHash,
    WebserviceEntry[] Bookmarks
);
