namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IErrorDeactivater
    {
        ErrorModel Deactivate(int errorId);
    }
}
