namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;

    public interface IDevLogInCheckProcessor
    {
        Task<DevCheckLogInRequest> Process(LogInRequest logInRequest);
    }
}