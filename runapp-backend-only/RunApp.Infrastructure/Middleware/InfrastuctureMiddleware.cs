using Microsoft.AspNetCore.Builder;

namespace RunApp.Infrastructure.Middleware
{
    public static class InfrastuctureMiddleware
    {
        public static IApplicationBuilder UseEventsInfrastructureMiddleware(this IApplicationBuilder appBuilder)
        {
           return appBuilder.UseMiddleware<EventsMiddleware>();
        }
    }
}
