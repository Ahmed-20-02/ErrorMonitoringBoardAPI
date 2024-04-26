namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    
    public interface ICreateLogPathCommand
    {
        Task<ErrorLogPath> Create(CreateLogPathModel request, int errorId);
    }
}