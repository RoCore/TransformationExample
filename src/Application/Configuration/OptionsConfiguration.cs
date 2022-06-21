using Domain.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

internal static class OptionsConfiguration
{
    /// <summary>
    /// Configure options with validation
    /// </summary>
    /// <typeparam name="T">Options to configure</typeparam>
    /// <param name="services">Service collection</param>
    /// <param name="validation">Options validation</param>
    public static IServiceCollection ConfigureOptionsWithValidation<T>(this IServiceCollection services,
        Func<T, bool> validation) where T : class, IConfigurableOptions
    {
        services.AddOptions<T>().BindConfiguration(typeof(T).Name).Validate(validation).ValidateOnStart();
        return services;
    }

    public static bool Validate(FileWatcherOptions options) => !string.IsNullOrEmpty(options?.Path) && !string.IsNullOrEmpty(options?.Filter);
    public static bool Validate(DeserializationOptions options) => options?.S50FileIdentifier?.Length == 3 && options?.S59FileIdentifier?.Length == 3;
}