namespace DevelopmentProjectErrorBoardAPI.Business.Creators
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Creators.Interfaces;

    public class ErrorCreator : IErrorCreator
    {
        private readonly ICreateErrorCommand _createErrorCommand;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorCreator(ICreateErrorCommand createErrorCommand,
            IErrorModelMapper errorModelMapper)
        {
            _errorModelMapper = errorModelMapper;
            _createErrorCommand = createErrorCommand;
        }

        public async Task<ErrorModel> Create(CreateErrorModel request)
        {
            try
            {
                var error = await this._createErrorCommand.Create(request);
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