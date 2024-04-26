namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    public interface IUpdateErrorStatusCommand
    {
        Task<Error> Update(int errorId, int statusId);
    }
}
