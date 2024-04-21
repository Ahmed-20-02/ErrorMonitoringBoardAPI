namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateErrorsAssignedDeveloper
    {
        Error Update(int errorId, int devId);
    }
}