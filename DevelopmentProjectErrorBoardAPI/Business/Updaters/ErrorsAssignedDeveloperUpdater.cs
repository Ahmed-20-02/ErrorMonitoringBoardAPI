namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class ErrorsAssignedDeveloperUpdater : IErrorsAssignedDeveloperUpdater
    {
        private readonly IUpdateErrorsAssignedDeveloperCommand _updateErrorsAssignedDeveloperCommand;
        private readonly IErrorModelMapper _errorModelMapper;

        public ErrorsAssignedDeveloperUpdater(IErrorModelMapper errorModelMapper, 
            IUpdateErrorsAssignedDeveloperCommand updateErrorsAssignedDeveloperCommand)
        {
            _errorModelMapper = errorModelMapper;
            _updateErrorsAssignedDeveloperCommand = updateErrorsAssignedDeveloperCommand;
        }

        public async Task<ErrorModel> Update(UpdateErrorsAssignedDeveloperRequest request)
        {
            try
            {
                var error = await this._updateErrorsAssignedDeveloperCommand.Update(request.ErrorId, request.DevId);
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