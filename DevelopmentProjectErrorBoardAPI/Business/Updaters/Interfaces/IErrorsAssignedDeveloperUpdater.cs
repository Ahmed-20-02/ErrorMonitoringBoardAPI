namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IErrorsAssignedDeveloperUpdater
    {
        Task<ErrorModel> Update(UpdateErrorsAssignedDeveloperModel model);
    }
}