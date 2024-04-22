namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    public interface IUpdateErrorStatus
    {
        Task<Error> Update(int errorId, int statusId, int devId);
    }
}
