namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IErrorStatusUpdater
    {
        Task<ErrorModel> Update(int errorId, int statusId, int devId);
    }
}