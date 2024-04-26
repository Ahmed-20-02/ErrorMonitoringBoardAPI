namespace DevelopmentProjectErrorBoardAPI.Business.Creators.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface ILogPathCreator
    {
        Task<ErrorLogPathModel> Create(CreateLogPathModel request, int errorId);
    }
}