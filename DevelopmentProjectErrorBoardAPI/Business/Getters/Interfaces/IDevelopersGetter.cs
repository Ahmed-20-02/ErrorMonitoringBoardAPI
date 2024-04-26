namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface IDevelopersGetter
    {
        Task<List<UserModel>> Get();
    }
}