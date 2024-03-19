namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;

    public class UnresolvedErrorsGetter : IUnresolvedErrorsGetter
    {
        private readonly IGetUnresolvedErrorsQuery _getUnresolvedErrorsQuery;

        public UnresolvedErrorsGetter(IGetUnresolvedErrorsQuery getUnresolvedErrorsQuery)
        {
            _getUnresolvedErrorsQuery = getUnresolvedErrorsQuery;
        }

        public List<Error> Get()
        {
            return _getUnresolvedErrorsQuery.Get().Result;
        }
    }
}