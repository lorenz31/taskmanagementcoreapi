using CoreApiProject.Core.BusinessModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiProject.Core.Services
{
    public interface ITasksService
    {
        Task<IResponseModel> AddTaskAsync(TasksModel obj);
        Task<List<TasksModel>> GetTasksPerProjectAsync(Guid projectid);
        Task<TasksModel> GetTaskDetailAsync(Guid projectid, Guid taskid);
        Task<bool> UpdateTaskStatusAsync(Guid projectid, Guid taskid);
    }
}