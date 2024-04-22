namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IDevelopersGetter
    {
        Task<List<UserModel>> Get();
    }
}