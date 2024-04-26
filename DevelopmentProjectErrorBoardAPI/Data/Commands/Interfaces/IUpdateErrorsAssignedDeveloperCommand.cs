namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateErrorsAssignedDeveloperCommand
    {
        Task<Error> Update(int errorId, int devId);
    }
}