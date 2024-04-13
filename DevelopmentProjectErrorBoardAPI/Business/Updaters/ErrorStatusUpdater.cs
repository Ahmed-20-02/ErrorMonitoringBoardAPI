
namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Data.Updaters;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;

    public class ErrorStatusUpdater : IErrorStatusUpdater
    {
        private readonly IUpdateErrorStatus _updateErrorStatus;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorStatusUpdater(IUpdateErrorStatus updateErrorStatus, 
            IErrorModelMapper errorModelMapper)
        {
            _updateErrorStatus = updateErrorStatus;
            _errorModelMapper = errorModelMapper;
        }

        public ErrorModel Update(int errorId, int statusId)
        {
            var error = this._updateErrorStatus.Update(errorId, statusId);
            var mappedError = _errorModelMapper.Map(error);
            
            return mappedError;
        }
    }
}