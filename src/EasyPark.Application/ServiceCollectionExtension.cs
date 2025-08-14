using System.Reflection;
using EasyPark.Application.CQRS;
using EasyPark.Application.UseCases;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace EasyPark.Application;

public static class ServiceCollectionExtension
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly, includeInternalTypes: true);

        services.AddScoped<ISender, Sender>();

        services.Scan(selector => selector
            .FromAssemblyOf<CreateOrderHandler>()
            .AddClasses(classes => classes.AssignableTo<IHandler>(), publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface().AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
