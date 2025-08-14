using EasyPark.Contracts;
using EasyPark.Infrastructure.Options;
using EasyPark.Infrastructure.Paypal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;
using Environment = PaypalServerSdk.Standard.Environment;

namespace EasyPark.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptionsWithValidateOnStart<PayPalOptions>().BindConfiguration(PayPalOptions.SectionName)
            .ValidateOnStart();

        services.AddScoped<IPaypalClient, PayPalClient>();
        services.AddSingleton<PaypalServerSdkClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<PayPalOptions>>().Value;
            var environment =
                options.Environment!.Equals(nameof(Environment.Sandbox), StringComparison.InvariantCultureIgnoreCase)
                    ? Environment.Sandbox
                    : Environment.Production;

            return new PaypalServerSdkClient.Builder()
                .ClientCredentialsAuth(
                    new ClientCredentialsAuthModel.Builder(options.ClientId, options.ClientSecret)
                        .Build()).Environment(environment)
                .LoggingConfig(config => config
                    .LogLevel(LogLevel.Information)
                    .RequestConfig(reqConfig => reqConfig.Body(true))
                    .ResponseConfig(respConfig => respConfig.Headers(true))
                )
                .Build();
        });
    }
}