namespace DevelopmentProjectErrorBoardAPI.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Creators.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;

    public class CreateErrorProcessor : ICreateErrorProcessor
    {
        private readonly IErrorCreator _errorCreator;
        private readonly ILogPathCreator _logPathCreator;

        public CreateErrorProcessor(IErrorCreator errorCreator, 
            ILogPathCreator logPathCreator)
        {
            _errorCreator = errorCreator;
            _logPathCreator = logPathCreator;
        }
        
        public async Task<ErrorAndPathModel> Process(CreateErrorRequest request)
        {
            try
            {
                ErrorAndPathModel response = new ErrorAndPathModel();
                response.LogPaths = new List<ErrorLogPathModel>();
                    
                var error = await _errorCreator.Create(request.Error);

                response.Error = error;

                if (error.ErrorId > 0)
                {
                    foreach (var logPath in request.LogPaths)
                    {
                        var mappedLogPath = await _logPathCreator.Create(logPath, error.ErrorId);
                        response.LogPaths.Add(mappedLogPath); 
                    }
                }

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}