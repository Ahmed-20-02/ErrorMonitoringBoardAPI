namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class ErrorStatusUpdater : IErrorStatusUpdater
    {
        private readonly IUpdateErrorStatusCommand _updateErrorStatusCommand;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorStatusUpdater(IUpdateErrorStatusCommand updateErrorStatusCommand, 
            IErrorModelMapper errorModelMapper)
        {
            _updateErrorStatusCommand = updateErrorStatusCommand;
            _errorModelMapper = errorModelMapper;
        }

        public async Task<ErrorModel> Update(int errorId, int statusId)
        {
            try
            {
                var error = await this._updateErrorStatusCommand.Update(errorId, statusId);
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