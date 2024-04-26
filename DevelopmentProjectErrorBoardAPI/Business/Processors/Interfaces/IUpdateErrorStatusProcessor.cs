namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    
    public interface IUpdateErrorStatusProcessor
    {
        Task<ErrorModel> Process(UpdateErrorStatusRequest request);
    }
}