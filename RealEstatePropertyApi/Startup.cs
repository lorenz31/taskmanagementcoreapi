using CoreApiProject.Core.BusinessModels;
using CoreApiProject.Core.Services;
using CoreApiProject.Core.Jwt;
using CoreApiProject.Core;
using CoreApiProject.DAL.DataContext;
using CoreApiProject.Services.Service;
using CoreApiProject.Infrastructure.Repository;
using CoreApiProject.Infrastructure.Service;
using CoreApiProject.Infrastructure.Services;
using CoreApiProject.Filters;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CoreApiProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin() //WithOrigins("http://localhost:4200")
                                                                           .AllowAnyMethod()
                                                                           .AllowAnyHeader()
                                                                           .AllowCredentials());
            });

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetSection("Database:ConnectionString").Value));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = Configuration.GetSection("AppConfiguration:Issuer").Value,
                            ValidAudience = Configuration.GetSection("AppConfiguration:Audience").Value,
                            IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetSection("JwtSettings:SecurityKey").Value)
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                //Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                //Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddScoped<ValidateUserIdFilter>();
            services.AddScoped<ValidateUserProjIdFilter>();
            services.AddScoped<ValidateProjIdFilter>();
            services.AddScoped<ValidateProjTaskIdFilter>();
            services.AddScoped<ValidateTaskIdFilter>();

            //services.AddTransient<IClientModel, ClientModel>();
            services.AddTransient<IResponseModel, ResponseModel>();
            //services.AddTransient<IProjectModel, ProjectModel>();
            services.AddTransient<IUserModel, UserModel>();
            //services.AddTransient<ITasksModel, TasksModel>();

            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ICommentService, CommentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
