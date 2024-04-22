namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateErrorsAssignedDeveloper
    {
        Task<Error> Update(int errorId, int devId);
    }
}