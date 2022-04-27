using Lanre.Infrastructure.Mediator;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Lanre.Infrastructure;

public static class Configure
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        return services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                ;
    }
}
