namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    
    public interface IErrorsAssignedDeveloperUpdater
    {
        Task<ErrorModel> Update(UpdateErrorsAssignedDeveloperRequest request);
    }
}