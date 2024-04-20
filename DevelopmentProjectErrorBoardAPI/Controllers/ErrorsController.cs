using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Data;
using DevelopmentProjectErrorBoardAPI.Data.Updaters;
using DevelopmentProjectErrorBoardAPI.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;

namespace DevelopmentProjectErrorBoardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorsController : ControllerBase
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IAllErrorsGetter _allErrorsGetter;
        private readonly IUnresolvedErrorsGetter _unresolvedErrorsGetter;
        private readonly IDevelopersGetter _developersGetter;
        private readonly IErrorStatusUpdater _errorStatusUpdater;
        private readonly IUserPasswordUpdater _userPasswordUpdater;
        private readonly IErrorsAssignedDeveloperUpdater _errorsAssignedDeveloperUpdater;
        private readonly IUpdateErrorStatusProcessor _updateErrorStatusProcessor;
        private readonly IDevLogInCheckProcessor _devLogInCheckProcessor;
        private readonly ILogger _logger;
        
        public ErrorsController(IDbContextFactory<DataContext> contextFactory, 
            IAllErrorsGetter allErrorsGetter, 
            ILogger logger, 
            IUnresolvedErrorsGetter unresolvedErrorsGetter, 
            IErrorStatusUpdater errorStatusUpdater, 
            IUpdateErrorStatusProcessor updateErrorStatusProcessor, 
            IDevLogInCheckProcessor devLogInCheckProcessor, 
            IUserPasswordUpdater userPasswordUpdater, 
            IDevelopersGetter developersGetter,
            IErrorsAssignedDeveloperUpdater errorsAssignedDeveloperUpdater)
        {
            _contextFactory = contextFactory;
            _allErrorsGetter = allErrorsGetter;
            _logger = logger;
            _unresolvedErrorsGetter = unresolvedErrorsGetter;
            _errorStatusUpdater = errorStatusUpdater;
            _updateErrorStatusProcessor = updateErrorStatusProcessor;
            _devLogInCheckProcessor = devLogInCheckProcessor;
            _userPasswordUpdater = userPasswordUpdater;
            _developersGetter = developersGetter;
            _errorsAssignedDeveloperUpdater = errorsAssignedDeveloperUpdater;
        }

        [HttpGet("GetAllErrors")]
        public IActionResult GetAllErrors()
        {
            _logger.Log("GetAllErrors Called");
            try
            {
                return new OkObjectResult(_unresolvedErrorsGetter.Get().ErrorsAndPaths);
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed");
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpGet("GetDevelopers")]
        public IActionResult GetDevelopers()
        {
            _logger.Log($"GetDevelopers Called");
            try
            {
                return new OkObjectResult(_developersGetter.Get());
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed");
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpPut("UpdateErrorStatus")]
        public IActionResult UpdateErrorStatus([FromBody] UpdateErrorStatusModel model)
        {
            _logger.Log($"GetAllErrors Called for ErrorId{model.ErrorId} updating to status {model.StatusId} ");
            try
            {
                var error = _updateErrorStatusProcessor.Process(model);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed for ErrorId{model.ErrorId} updating to status {model.StatusId} ");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("UpdateErrorsAssignedDeveloper")]
        public IActionResult UpdateErrorsAssignedDeveloper([FromBody] UpdateErrorsAssignedDeveloperModel model)
        {
            _logger.Log($"UpdateErrorsAssignedDeveloper Called for ErrorId{model.ErrorId} updating to dev {model.DevId} ");
            try
            {
                var error = _errorsAssignedDeveloperUpdater.Update(model);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"UpdateErrorsAssignedDeveloper Failed for ErrorId{model.ErrorId} updating to dev {model.DevId} ");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("DevLogIn")]
        public IActionResult DevLogIn([FromBody] LogInModel model)
        {
            _logger.Log($"LogIn Called for user email {model.EmailAddress}");
            try
            {
                var logInModel = _devLogInCheckProcessor.Process(model);
                return new OkObjectResult(logInModel);
            }
            catch (Exception e)
            {
                _logger.Log($"LogIn Failed for user email {model.EmailAddress}");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("TestingUpdatePassword")]
        public IActionResult TestingUpdatePassword(int userId, string password)
        {
            try
            {
                return new OkObjectResult(_userPasswordUpdater.Update(userId, password));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
