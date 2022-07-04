using Atria.Models;

namespace Atria;

public class UserManager {
    private static readonly string[] Titles = { "", "Dr.", "Dr. Dr.", "Dr. Dr. Dr.", "Dr. mult." };
    private static readonly string[] FirstNames = {
        "Karin", "Hans",        "Renate", "Peter",        "Monika", "Klaus",        "Ursula", "Wolfgang",        "Ingrid", "Jürgen",        "Helga", "Dieter",        "Giesela", "Manfred",        "Elke", "Uwe",        "Brigitte", "Günther",        "Christa", "Horst",        "Hannelore", "Bernd",        "Bärbel", "Karl",        "Barbara", "Werner",        "Erika", "Heinz",        "Jutta", "Rolf",        "Christel", "Reiner",        "Heike", "Gerhard",        "Marion", "Helmuth",        "Angelika", "Michael",        "Inge", "Gerd",
        "Hyunji", "Jonas",
        "Selina", "Johannes",
                  "Stefan",
    };
    private static readonly string[] LastNames = {
        "Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Meyer", "Wagner", "Becker",
        "Schulz", "Hoffmann", "Schäfer", "Koch", "Bauer", "Richter", "Klein", "Wolf", "Schröder",
        "Neumann", "Schwarz", "Zimmermann", "Braun", "Krüger", "Hofmann", "Hartmann", "Lange",
    };

    private readonly List<User> _users;

    public IReadOnlyList<User> Users => _users;

    public UserManager() {
        _users = Enumerable.Range(0, 10 + Random.Shared.Next(90))
            .Select(_ => new User(Titles.RandomElement(),
                                  string.Join(' ', Enumerable.Range(0, (int)Math.Max(1, -Math.Log(Random.Shared.NextDouble())))
                                    .Select(_ => FirstNames.RandomElement())),
                                  LastNames.RandomElement()))
            .ToList();
    }

    public void Add(User user) => _users.Add(user);
}
