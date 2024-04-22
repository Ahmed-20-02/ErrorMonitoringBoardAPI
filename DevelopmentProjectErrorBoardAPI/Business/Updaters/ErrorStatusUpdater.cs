namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

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

        public async Task<ErrorModel> Update(int errorId, int statusId, int devId)
        {
            try
            {
                var error = await this._updateErrorStatus.Update(errorId, statusId, devId);
                var mappedError = _errorModelMapper.Map(error);
            
                return mappedError;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}