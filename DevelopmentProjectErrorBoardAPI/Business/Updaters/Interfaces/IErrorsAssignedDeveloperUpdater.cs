namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IErrorsAssignedDeveloperUpdater
    {
        ErrorModel Update(UpdateErrorsAssignedDeveloperModel model);
    }
}