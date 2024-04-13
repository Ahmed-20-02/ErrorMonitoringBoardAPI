namespace DevelopmentProjectErrorBoardAPI.Data.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    public interface IUpdateErrorStatus
    {
        Error Update(int errorId, int statusId);
    }
}
