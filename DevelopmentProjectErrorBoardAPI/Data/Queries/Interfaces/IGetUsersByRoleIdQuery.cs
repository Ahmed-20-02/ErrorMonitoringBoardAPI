namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetUsersByRoleIdQuery
    {
        Task<List<User>> Get(int roleId);
    }
}