namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface ICreateErrorCommand
    {
        Task<Error> Create(CreateErrorModel request);
    }
}