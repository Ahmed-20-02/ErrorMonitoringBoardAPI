namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IDeactivateError
    {
        Error Deactivate(int errorId);
    }
}