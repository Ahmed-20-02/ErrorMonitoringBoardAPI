namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;

    public class AllErrorsGetter : IAllErrorsGetter
    {
        private readonly IGetAllErrorsQuery _getAllErrorsQuery;

        public AllErrorsGetter(IGetAllErrorsQuery getAllErrorsQuery)
        {
            _getAllErrorsQuery = getAllErrorsQuery;
        }

        public List<Error> Get()
        {
            return _getAllErrorsQuery.Get().Result;
        }
    }
}