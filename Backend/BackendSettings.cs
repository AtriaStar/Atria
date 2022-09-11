using Backend.Services;
using Models;

namespace Backend;

public record BackendSettings(
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
);

public static class SettingsExtensions {
    public static T CreateAtriaOptions<T>(this IConfigurationRoot root)
        where T : class
        => root.GetRequiredSection("Atria").Get<T>()
           ?? throw new ArgumentException("Not all necessary options are set", nameof(T));

    public static IConfigurationBuilder AddStandardSources(this IConfigurationBuilder builder, string envName)
        => builder.SetBasePath(RootDirectory.Get())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{envName}.json", true, true);
}
