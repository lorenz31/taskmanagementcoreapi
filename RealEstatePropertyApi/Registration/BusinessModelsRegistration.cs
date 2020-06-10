using CoreApiProject.Core.BusinessModels;

using Microsoft.Extensions.DependencyInjection;

namespace CoreApiProject.Registration
{
    public static class BusinessModelsRegistration
    {
        public static void RegisterBusinessModels(this IServiceCollection services)
        {
            //services.AddTransient<IClientModel, ClientModel>();
            services.AddTransient<IResponseModel, ResponseModel>();
            //services.AddTransient<IProjectModel, ProjectModel>();
            services.AddTransient<IUserModel, UserModel>();
            //services.AddTransient<ITasksModel, TasksModel>();
        }
    }
}
