using Backend.Services;
using Models;

namespace Backend;

public record BackendOptions(
    string ApiPrefix,
    
    bool ShouldUseSwagger,
    string DatabaseString,
    bool DatabaseDetailedErrors,
    string AllowedOrigin,
    TimeSpan AuthenticationTokenExpireDuration,
    TimeSpan ResetTokenExpireDuration,

    double MinimumWseScore,
    double MinimumUserScore,
    
    int ScorePower,
    // TODO: https://github.com/dotnet/runtime/pull/74981
    FuzzingService.FactorWeights Weights = null!
) : SharedOptions(ApiPrefix);
