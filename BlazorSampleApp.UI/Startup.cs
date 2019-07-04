using BlazorSampleApp.UI.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSampleApp.UI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ScrollService>();
            services.AddSingleton<GithubService>();
            services.AddTransient<TodoService>();
            services.AddTransient<ConfigService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
