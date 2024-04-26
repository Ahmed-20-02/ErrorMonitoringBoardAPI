namespace DevelopmentProjectErrorBoardAPI.Business.Creators.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface IErrorCreator
    {
        Task<ErrorModel> Create(CreateErrorModel request);
    }
}