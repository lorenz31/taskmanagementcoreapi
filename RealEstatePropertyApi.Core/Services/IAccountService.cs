using CoreApiProject.Core.BusinessModels;

using System.Threading.Tasks;

namespace CoreApiProject.Core.Services
{
    public interface IAccountService
    {
        Task<IResponseModel> RegisterUserAsync(IUserModel model);
        Task<UserModel> VerifyUserAsync(IUserModel model);
        TokenModel GenerateJwt(IUserModel model);

        //Task<IResponseModel> RegisterClientAsync(ClientModel model);
        //Task<ClientModel> VerifyClientAsync(ClientModel model);
        //TokenModel GenerateJwt(ClientModel model);
    }
}