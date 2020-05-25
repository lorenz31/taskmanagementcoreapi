using CoreApiProject.Core.BusinessModels;

using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CoreApiProject.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private IConfiguration _config;

        public Repository(IConfiguration config)
        {
            _config = config;
        }

        #region Projects
        public async Task<List<ProjectModel>> GetProjectsAsync(Guid userid)
        {
            try
            {
                using (var con = new SqlConnection(_config["Database:ConnectionString"]))
                {
                    con.Open();

                    var projects = await con.QueryAsync<ProjectModel>("sp_GetAllUserProjects", new { UserId = userid }, commandType: CommandType.StoredProcedure);

                    con.Close();

                    return projects.AsList() ?? null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ProjectModel>> GetActiveProjectsAsync(Guid userid)
        {
            try
            {
                using (var con = new SqlConnection(_config["Database:ConnectionString"]))
                {
                    con.Open();

                    var projects = await con.QueryAsync<ProjectModel>("sp_GetUserActiveProjects", new { UserId = userid }, commandType: CommandType.StoredProcedure);

                    con.Close();

                    return projects.AsList() ?? null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Members
        public async Task<List<MemberModel>> GetMembersPerUserAsync(Guid userid)
        {
            try
            {
                using (var con = new SqlConnection(_config["Database:ConnectionString"]))
                {
                    con.Open();

                    var members = await con.QueryAsync<MemberModel>("sp_GetMembersPerUser", new { UserId = userid }, commandType: CommandType.StoredProcedure);

                    con.Close();

                    return members.AsList() ?? null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Tasks
        public async Task<List<TasksModel>> GetTasksPerProjectAsync(Guid projectid)
        {
            try
            {
                using (var con = new SqlConnection(_config["Database:ConnectionString"]))
                {
                    con.Open();

                    var tasks = await con.QueryAsync<TasksModel>("sp_GetProjectTasks", new { ProjectId = projectid }, commandType: CommandType.StoredProcedure);

                    con.Close();

                    return tasks.AsList() ?? null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TasksModel> GetTaskDetailAsync(Guid projectid, Guid taskid)
        {
            try
            {
                using (var con = new SqlConnection(_config["Database:ConnectionString"]))
                {
                    con.Open();

                    var task = await con.QueryFirstOrDefaultAsync<TasksModel>("sp_GetTaskDetail", new { ProjectId = projectid, TaskId = taskid }, commandType: CommandType.StoredProcedure);

                    con.Close();

                    return task ?? null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Comments
        public async Task<List<ViewCommentModel>> GetCommentsPerTaskAsync(Guid taskid)
        {
            try
            {
                using (var con = new SqlConnection(_config["Database:ConnectionString"]))
                {
                    con.Open();

                    var comments = await con.QueryAsync<ViewCommentModel>("sp_GetTaskComments", new { TaskId = taskid }, commandType: CommandType.StoredProcedure);

                    con.Close();

                    return comments.AsList() ?? null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
