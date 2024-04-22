namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class ErrorsAssignedDeveloperUpdater : IErrorsAssignedDeveloperUpdater
    {
        private readonly IUpdateErrorsAssignedDeveloper _updateErrorsAssignedDeveloper;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorsAssignedDeveloperUpdater(IErrorModelMapper errorModelMapper, 
            IUpdateErrorsAssignedDeveloper updateErrorsAssignedDeveloper)
        {
            _errorModelMapper = errorModelMapper;
            _updateErrorsAssignedDeveloper = updateErrorsAssignedDeveloper;
        }

        public async Task<ErrorModel> Update(UpdateErrorsAssignedDeveloperModel model)
        {
            try
            {
                var error = await this._updateErrorsAssignedDeveloper.Update(model.ErrorId, model.DevId);
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