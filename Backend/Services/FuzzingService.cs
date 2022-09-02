using FuzzierSharp.PreProcess;
using FuzzierSharp.SimilarityRatio.Scorer;
using FuzzierSharp.SimilarityRatio.Scorer.StrategySensitive;
using Models;

namespace Backend.Services;

public class FuzzingService {
    public record FactorWeights(int Name, int ShortDescription, int FullDescription, int Changelog);

    private record Factor(Func<WebserviceEntry, string?> Mapper, int Weight);

    private static readonly IRatioScorer _scorer = new PartialRatioScorer();

    private readonly Factor[] _factors;
    private readonly int _scorePower;

    public FuzzingService(BackendOptions opt) {
        var weights = opt.Weights;
        _factors = new Factor[] {
            new(w => w.Name, weights.Name),
            new(w => w.ShortDescription, weights.ShortDescription),
            new(w => w.FullDescription, weights.FullDescription),
            new(w => w.ChangeLog, weights.Changelog),
        };
        _scorePower = opt.ScorePower;
    }

    /// <param name="query"></param>
    /// <param name="entry"></param>
    /// <returns>Score in [0, 1].</returns>
    // TODO: Consider tags, questions, etc.
    public double CalculateScore1(string query, WebserviceEntry entry) {
        var (totalScore, totalWeight) = _factors
            .Select(x => (str: x.Mapper(entry), x.Weight))
            .Where(x => x.str is not null)
            .Aggregate((totalScore: 0.0, totalWeight: 0), (s, x) => (
                s.totalScore += Math.Pow(_scorer.Score(query, x.str, StandardPreprocessors.CaseInsensitive) / 100.0, _scorePower) * x.Weight,
                s.totalWeight += x.Weight));
        return totalScore / totalWeight;
    }

    public double CalculateScore2(string query, WebserviceEntry entry)
        => _factors
            .Select(x => (str: x.Mapper(entry), x.Weight))
            .Where(x => x.str is not null)
            .Select(x => Math.Pow(_scorer.Score(query, x.str) / 100.0, _scorePower))
            .Max();

    public double CalculateScore(string query, User user)
        => Math.Max(_scorer.Score(query, user.Email),
            _scorer.Score(query, $"{user.Title ?? string.Empty} {user.FirstNames} {user.LastName}"));
}
