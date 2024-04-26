namespace DevelopmentProjectErrorBoardAPI.Business.Creators
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Creators.Interfaces;

    public class LogPathCreator : ILogPathCreator
    {
        private readonly ICreateLogPathCommand _logPathCommand;
        private readonly IErrorLogPathModelMapper _logPathModelMapper;

        public LogPathCreator(ICreateLogPathCommand logPathCommand, 
            IErrorLogPathModelMapper logPathModelMapper)
        {
            _logPathCommand = logPathCommand;
            _logPathModelMapper = logPathModelMapper;
        }


        public async Task<ErrorLogPathModel> Create(CreateLogPathModel request, int errorId)
        {
            try
            {
                var error = await this._logPathCommand.Create(request, errorId);
                var mappedError = _logPathModelMapper.Map(error);
            
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