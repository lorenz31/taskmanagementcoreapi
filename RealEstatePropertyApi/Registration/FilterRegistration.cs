using CoreApiProject.Filters;

using Microsoft.Extensions.DependencyInjection;

namespace CoreApiProject.Registration
{
    public static class FilterRegistration
    {
        public static void RegisterFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidateUserIdFilter>();
            services.AddScoped<ValidateUserProjIdFilter>();
            services.AddScoped<ValidateProjIdFilter>();
            services.AddScoped<ValidateProjTaskIdFilter>();
            services.AddScoped<ValidateTaskIdFilter>();
        }
    }
}
