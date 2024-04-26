namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface IErrorCloser
    {
        Task<ErrorModel> Close(int errorId);
    }
}
