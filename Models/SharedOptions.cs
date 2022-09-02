using Microsoft.Extensions.Configuration;

namespace Models;

public record SharedOptions(
    string ApiPrefix
);

public static class OptionExtensions {
    public static T CreateAtriaOptions<T>(this IConfigurationRoot root)
        where T : class
        => root.GetRequiredSection("Atria").Get<T>()
           ?? throw new ArgumentException("Not all necessary options are set", nameof(T));

    public static IConfigurationBuilder AddStandardSources(this IConfigurationBuilder builder, string envName)
        => builder.SetBasePath(RootDirectory.Get())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{envName}.json", true, true);
}
