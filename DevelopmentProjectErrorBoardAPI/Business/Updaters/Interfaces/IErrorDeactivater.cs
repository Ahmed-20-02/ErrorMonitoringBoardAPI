namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IErrorDeactivater
    {
        Task<ErrorModel> Deactivate(int errorId);
    }
}
