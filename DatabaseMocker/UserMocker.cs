using Backend.AspPlugins;
using Models;

namespace DatabaseMocker;

public class UserMocker {
    private static string?[] Titles = {
        null, "Dr.", "Dr. Dr.", "Dr. mult.",
    };

    private static string[] FirstNames = {
        "Ernst", "Friedrich", "Hans", "Heinrich", "Hermann", "Karl", "Otto", "Paul", "Walter", "Wilhelm",
        "Gerhard", "Günter", "Hans", "Heinz", "Helmut", "Herbert", "Karl", "Kurt", "Walter", "Werner",
        "Dieter", "Günter", "Hans", "Horst", "Jürgen", "Klaus", "Manfred", "Peter", "Uwe", "Wolfgang",
        "Andreas", "Frank", "Jörg", "Jürgen", "Klaus", "Michael", "Peter", "Stefan", "Thomas", "Uwe",
        "Alexander", "Christian", "Daniel", "Dennis", "Jan", "Martin", "Michael", "Sebastian", "Stefan", "Thomas",
        "Finn", "Jan", "Jannik", "Jonas", "Leon", "Luca", "Lukas", "Niklas", "Tim", "Tom",
        "Anna", "Bertha", "Elisabeth", "Emma", "Frieda ", "Gertrud", "Margarethe", "Maria", "Marie", "Martha",
        "Edith", "Elfriede", "Erna", "Gerda", "Gertrud", "Hildegard", "Ilse", "Irmgard", "Lieselotte", "Ursula",
        "Christa", "Elke", "Erika", "Gisela", "Helga", "Ingrid", "Karin", "Monika", "Renate", "Ursula",
        "Andrea", "Angelika", "Birgit", "Gabriele", "Heike", "Martina", "Petra", "Sabine", "Susanne", "Ute",
        "Anja", "Christina", "Julia", "Katrin", "Melanie", "Nadine", "Nicole", "Sabrina", "Sandra", "Stefanie",
        "Anna", "Hannah", "Julia", "Lara", "Laura", "Lea", "Lena", "Lisa", "Michelle", "Sarah",
        "Mia", "Emma", "Hannah", "Sophia", "Emilia", "Anna", "Lea", "Lina", "Lena", "Marie",
        "Hyunji", "Selina", "Johannes", "Jonas", "Stefan",
    };

    private static string[] LastNames = {
        "Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Schäfer", "Meyer", "Wagner", "Becker", "Bauer",
        "Hoffmann", "Schulz", "Koch", "Richter", "Klein", "Wolf", "Schröder", "Neumann", "Braun", "Werner", "Schwarz",
        "Hofmann", "Zimmermann", "Schmitt", "Hartmann", "Schmid", "Weiß", "Schmitz", "Krüger", "Lange", "Meier",
        "Walter", "Köhler", "Maier", "Beck", "König", "Krause", "Schulze", "Huber", "Mayer", "Frank", "Lehmann",
        "Kaiser", "Fuchs", "Herrmann", "Lang", "Thomas", "Peters", "Stein", "Jung", "Möller", "Berger", "Martin",
        "Friedrich", "Scholz", "Keller", "Groß", "Hahn", "Roth", "Günther", "Vogel", "Schubert", "Winkler", "Schuster",
        "Lorenz", "Ludwig", "Baumann", "Heinrich", "Otto", "Simon", "Graf", "Kraus", "Krämer", "Böhm", "Schulte",
        "Albrecht", "Franke", "Winter", "Schumacher", "Vogt", "Haas", "Sommer", "Schreiber", "Engel", "Ziegler",
        "Dietrich", "Brandt", "Seidel", "Kuhn", "Busch", "Horn", "Arnold", "Kühn", "Bergmann", "Pohl", "Pfeiffer",
        "Wolff", "Voigt", "Sauer",
    };

    public static async Task<int> AddUser(AtriaContext context)
    {
        User userTest = new User
        {
            FirstNames = "Testi",
            LastName = "Test",
            Email = "test@test.de",
            Biography = "Hi ich bin ein Test, bitte sei lieb zu mir :'(",
            PasswordHash = Array.Empty<byte>(),
            PasswordSalt = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
            Title = Titles.RandomElement()
        };

        await context.Users.AddAsync(userTest);
        return await context.SaveChangesAsync();



    }


        public static User GenerateUser() {
        var firstNames = Enumerable.Range(0, (int) Math.Ceiling(0.1 / Random.Shared.NextDouble() + 0.9))
            .Select(_ => FirstNames.RandomElement())
            .ToArray();
        var lastName = LastNames.RandomElement();
        return new() {
            Email = $"{firstNames.First()}.{lastName}@atria.de".ToLowerInvariant(),
            FirstNames = string.Join(' ', firstNames),
            LastName = lastName,
            PasswordHash = Array.Empty<byte>(),
            PasswordSalt = Array.Empty<byte>(),
            SignUpIp = "127.0.0.1",
            Title = Titles.RandomElement(),
        };
    }
}
