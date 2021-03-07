using ElectronicVoting.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.SignalR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSignalRHubs (this IServiceCollection services)
        {
            services.AddSignalR().AddMessagePackProtocol();
            return services;
        }
        
        public static IApplicationBuilder UseSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(a => a.MapHub<ValidationServerManagerHub>("/ValidationServerManagerHub"));
            return app;
        }
    }
}