namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;

    public class ErrorDeactivater : IErrorDeactivater
    {
        private readonly IDeactivateError _deactivateError;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorDeactivater(IDeactivateError deactivateError, 
            IErrorModelMapper errorModelMapper)
        {
            _deactivateError = deactivateError;
            _errorModelMapper = errorModelMapper;
        }

        public async Task<ErrorModel> Deactivate(int errorId)
        {
            try
            {
                var error = await this._deactivateError.Deactivate(errorId);
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