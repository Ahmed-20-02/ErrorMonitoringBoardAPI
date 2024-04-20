namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Data.Updaters.Interfaces;

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

        public ErrorModel Update(UpdateErrorsAssignedDeveloperModel model)
        {
            var error = this._updateErrorsAssignedDeveloper.Update(model.ErrorId, model.DevId);
            var mappedError = _errorModelMapper.Map(error);
            
            return mappedError;
        }
    }
}