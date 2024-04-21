namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;    
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;

    public class ActiveErrorsGetter : IActiveErrorsGetter
    {
        private readonly IGetActiveErrorsQuery _getActiveErrorsQuery;
        private readonly IGetLogPathForErrorQuery _getLogPathForErrorQuery;
        private readonly IErrorModelMapper _errorModelMapper;
        private readonly IErrorLogPathModelMapper _errorLogPathModelMapper;

        public ActiveErrorsGetter(IGetActiveErrorsQuery getActiveErrorsQuery, 
            IGetLogPathForErrorQuery getLogPathForErrorQuery, 
            IErrorModelMapper errorModelMapper, 
            IErrorLogPathModelMapper errorLogPathModelMapper)
        {
            _getActiveErrorsQuery = getActiveErrorsQuery;
            _getLogPathForErrorQuery = getLogPathForErrorQuery;
            _errorModelMapper = errorModelMapper;
            _errorLogPathModelMapper = errorLogPathModelMapper;
        }

        public List<ErrorAndPathModel> Get()
        {
            try
            {
                var unresolvedErrors = _getActiveErrorsQuery.Get().Result;
                var errors = new List<ErrorAndPathModel>();

                for (int i = 0; i < unresolvedErrors.Count; i++)
                {
                    var logPaths = _getLogPathForErrorQuery.Get(unresolvedErrors[i].ErrorId);

                    var errorPathModel = new ErrorAndPathModel();
                    errorPathModel.Error = _errorModelMapper.Map(unresolvedErrors[i]);
                    errorPathModel.LogPaths = new List<ErrorLogPathModel>();

                    if (logPaths.Result.Count > 0)
                    {
                        for (int y = 0; y < logPaths.Result.Count; y++)
                        {
                            errorPathModel.LogPaths.Add(_errorLogPathModelMapper.Map(logPaths.Result[y]));
                        }
                    }
                
                    errors.Add(errorPathModel);
                }

                return errors;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}