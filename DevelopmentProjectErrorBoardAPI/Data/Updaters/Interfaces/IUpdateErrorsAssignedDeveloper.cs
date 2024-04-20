
namespace DevelopmentProjectErrorBoardAPI.Data.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateErrorsAssignedDeveloper
    {
        Error Update(int errorId, int devId);
    }
}