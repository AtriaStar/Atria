using FuzzySharp;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Scorer.Composite;
using Models;

namespace Backend.Services; 

public class FuzzingService {
    private static readonly WeightedRatioScorer _scorer = new();

    private static readonly Factor[] Factors = {
        new(w => w.Name, 100),
        new(w => w.ShortDescription, 50),
        new(w => w.FullDescription, 30),
        new(w => w.Changelog, 5),
    };

    /// <param name="query"></param>
    /// <param name="entry"></param>
    /// <returns>Score in [0, 1].</returns>
    public static double CalculateScore(string query, WebserviceEntry entry) {
        // TODO: Consider tags, questions, etc.
        var (totalScore, totalWeight) = Factors
            .Select(x => (str: x.Mapper(entry), x.Weight))
            .Where(x => x.str is not null)
            .Aggregate((totalScore: 0.0, totalWeight: 0), (s, x) => (
                s.totalScore += _scorer.Score(query, x.str) / 100.0 * x.Weight,
                s.totalWeight += x.Weight));
        return totalScore / totalWeight;
    }

    public static double CalculateScore(string query, User user)
        => Math.Max(_scorer.Score(query, user.Email),
            _scorer.Score(query, $"{user.Title ?? string.Empty} {user.FirstNames} {user.LastName}"));

    private record Factor(Func<WebserviceEntry, string?> Mapper, int Weight);
}
