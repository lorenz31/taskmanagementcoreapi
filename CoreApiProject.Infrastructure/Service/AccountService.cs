using CoreApiProject.Core.Models;
using CoreApiProject.Core.Jwt;
using CoreApiProject.Core.BusinessModels;
using CoreApiProject.Core.Services;
using CoreApiProject.Core.Security;
using CoreApiProject.DAL.DataContext;

using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CoreApiProject.Services.Service
{
    public class AccountService : IAccountService
    {
        private DatabaseContext _db;
        private IConfiguration _config;

        private IResponseModel _response;

        public AccountService(
            DatabaseContext db,
            IConfiguration config,
            IResponseModel response)
        {
            _db = db;
            _config = config;
            _response = response;
        }

        #region Client Authentication
        //public async Task<IResponseModel> RegisterClientAsync(ClientModel model)
        //{
        //    try
        //    {
        //        var client = new Client
        //        {
        //            Id = Guid.NewGuid(),
        //            Email = model.Email,
        //            ApiKey = Guid.NewGuid()
        //        };

        //        _db.Clients.Add(client);
        //        await _db.SaveChangesAsync();

        //        _response.Status = true;
        //        _response.Message = "Client registration successful.";

        //        return _response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Status = false;
        //        _response.Message = "Client registration error.";

        //        return _response;
        //    }
        //}

        //public async Task<ClientModel> VerifyClientAsync(ClientModel model)
        //{
        //    try
        //    {
        //        var clientInfo = await _db.Clients.Where(u => u.Email == model.Email && u.ApiKey == model.ApiKey).SingleOrDefaultAsync();

        //        if (clientInfo == null) return null;

        //        return new ClientModel
        //        {
        //            Email = clientInfo.Email,
        //            ClientId = clientInfo.Id
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public TokenModel GenerateJwt(ClientModel model)
        //{
        //    var token = new JwtTokenBuilder(_config);

        //    return new TokenModel
        //    {
        //        AccessToken = token.GenerateToken(),
        //        Email = model.Email
        //    };
        //}
        #endregion

        #region User Authentication
        public async Task<IResponseModel> RegisterUserAsync(IUserModel model)
        {
            try
            {
                var salt = PasswordHash.GenerateSalt();
                var passwordHash = PasswordHash.ComputeHash(model.Password, salt);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    //ClientId = ClientAppHelper.ClientApp,
                    Username = model.Username,
                    Password = Convert.ToBase64String(passwordHash),
                    Salt = Convert.ToBase64String(salt)
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                _response.Status = true;
                _response.Message = "User registration successful.";

                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = "User registration error.";

                return _response;
            }
        }

        public async Task<UserModel> VerifyUserAsync(IUserModel model)
        {
            try
            {
                var userInfo = await _db.Users.Where(u => u.Username == model.Username).SingleOrDefaultAsync();

                if (userInfo != null)
                {
                    var salt = Convert.FromBase64String(userInfo.Salt);
                    var hashPassword = Convert.FromBase64String(userInfo.Password);
                    var isVerified = PasswordHash.VerifyPassword(model.Password, salt, hashPassword);

                    if (!isVerified)
                        return null;
                    else
                    {
                        return new UserModel
                        {
                            UserId = userInfo.Id,
                            Username = userInfo.Username
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TokenModel GenerateJwt(IUserModel model)
        {
            var token = new JwtTokenBuilder(_config);

            return new TokenModel
            {
                AccessToken = token.GenerateToken(),
                UserId = model.UserId,
                Email = model.Username
            };
        }
        #endregion
    }
}