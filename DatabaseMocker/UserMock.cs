using Backend;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace DatabaseMocker;

public class UserMock {
    public static async Task<EntityEntry<User>> AddUser(AtriaContext context) {
        User user1 = new User {
            FirstNames = "John",
            LastName = "Smith",
            Email = "floppa@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user2 = new User {
            FirstNames = "Gerald",
            LastName = "of Rivia",
            Email = "geraldOfRivia@rivia.com",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.2",
        };

        User user3 = new User {
            FirstNames = "J. R. R.",
            LastName = "Tolkien",
            Email = "j.tolkien@middle-earth.com",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.3",
        };

        User user4 = new User {
            FirstNames = "Jane",
            LastName = "Smith",
            Email = "floppo@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.4",
        };

        User user5 = new User {
            FirstNames = "John",
            LastName = "Doe",
            Email = "floppi@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user6 = new User {
            FirstNames = "Johnny",
            LastName = "Smith",
            Email = "floppu@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user7 = new User {
            FirstNames = "Johanna",
            LastName = "Smith",
            Email = "floppe@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user8 = new User {
            FirstNames = "Johann",
            LastName = "Smith",
            Email = "flopp@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user9 = new User {
            FirstNames = "James",
            LastName = "Bond",
            Email = "jb@floppa.de",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user10 = new User {
            FirstNames = "Michael",
            LastName = "Scofield",
            Email = "misco@break.com",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user11 = new User {
            FirstNames = "Sherlock",
            LastName = "Holmes",
            Email = "sh@london.at",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user12 = new User {
            Title = "Dr.",
            FirstNames = "John",
            LastName = "Watson",
            Email = "jw@london.at",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user13 = new User {
            Title = "Dr.",
            FirstNames = "Meredith",
            LastName = "Grey",
            Email = "megr@seatlle.us",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user14 = new User {
            FirstNames = "Tyrion",
            LastName = "Lannister",
            Email = "tlannister@lan.got",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user15 = new User {
            FirstNames = "Cersei",
            LastName = "Lannister",
            Email = "clannister@lan.got",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };

        User user16 = new User {
            FirstNames = "Jaime",
            LastName = "Lannister",
            Email = "jlannister@lan.got",
            PasswordSalt = Array.Empty<byte>(),
            PasswordHash = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
        };
        
        await context.Users.AddAsync(user1);
        await context.Users.AddAsync(user2);
        await context.Users.AddAsync(user3);
        await context.Users.AddAsync(user4);
        await context.Users.AddAsync(user5);
        await context.Users.AddAsync(user6);
        await context.Users.AddAsync(user7);
        await context.Users.AddAsync(user8);
        await context.Users.AddAsync(user9);
        await context.Users.AddAsync(user10);
        await context.Users.AddAsync(user11);
        await context.Users.AddAsync(user12);
        await context.Users.AddAsync(user13);
        await context.Users.AddAsync(user14);
        await context.Users.AddAsync(user15);
        return await context.Users.AddAsync(user16);
    }
}