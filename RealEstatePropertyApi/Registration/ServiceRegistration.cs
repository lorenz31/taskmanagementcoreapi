using CoreApiProject.Core.Services;
using CoreApiProject.Infrastructure.Repository;
using CoreApiProject.Infrastructure.Service;

using Microsoft.Extensions.DependencyInjection;

namespace CoreApiProject.Registration
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ICommentService, CommentService>();
        }
    }
}
