namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;

    public interface ICreateErrorProcessor
    {
        Task<ErrorAndPathModel> Process(CreateErrorRequest request);
    }
}