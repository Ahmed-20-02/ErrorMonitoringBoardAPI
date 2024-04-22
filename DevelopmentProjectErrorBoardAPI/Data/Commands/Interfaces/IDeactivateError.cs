namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IDeactivateError
    {
        Task<Error> Deactivate(int errorId);
    }
}