namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IErrorStatusUpdater
    {
        ErrorModel Update(int errorId, int statusId, int devId);
    }
}