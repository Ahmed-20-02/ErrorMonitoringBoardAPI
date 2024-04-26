namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface ICloseErrorCommand
    {
        Task<Error> Close(int errorId);
    }
}