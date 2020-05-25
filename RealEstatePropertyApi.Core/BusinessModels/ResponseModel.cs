namespace CoreApiProject.Core.BusinessModels
{
    public class ResponseModel : IResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}