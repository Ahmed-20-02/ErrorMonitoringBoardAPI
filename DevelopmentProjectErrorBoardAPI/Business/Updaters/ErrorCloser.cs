namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;

    public class ErrorCloser : IErrorCloser
    {
        private readonly ICloseErrorCommand _closeErrorCommand;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorCloser(ICloseErrorCommand closeErrorCommand, 
            IErrorModelMapper errorModelMapper)
        {
            _closeErrorCommand = closeErrorCommand;
            _errorModelMapper = errorModelMapper;
        }

        public async Task<ErrorModel> Close(int errorId)
        {
            try
            {
                var error = await this._closeErrorCommand.Close(errorId);
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