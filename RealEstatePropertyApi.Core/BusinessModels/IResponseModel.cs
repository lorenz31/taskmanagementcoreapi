namespace CoreApiProject.Core.BusinessModels
{
    public interface IResponseModel
    {
        bool Status { get; set; }
        string Message { get; set; }
    }
}
