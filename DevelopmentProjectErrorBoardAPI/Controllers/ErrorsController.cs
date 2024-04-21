using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Data;
using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
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
        private readonly IActiveErrorsGetter _activeErrorsGetter;
        private readonly IDevelopersGetter _developersGetter;
        private readonly IProjectsGetter _projectsGetter;
        private readonly IUserPasswordUpdater _userPasswordUpdater;
        private readonly IErrorsAssignedDeveloperUpdater _errorsAssignedDeveloperUpdater;
        private readonly IUpdateErrorStatusProcessor _updateErrorStatusProcessor;
        private readonly IDeactivateError _deactivateError;
        private readonly IDevLogInCheckProcessor _devLogInCheckProcessor;
        private readonly ILogger _logger;
        
        public ErrorsController(
            ILogger logger, 
            IActiveErrorsGetter activeErrorsGetter, 
            IUpdateErrorStatusProcessor updateErrorStatusProcessor, 
            IDevLogInCheckProcessor devLogInCheckProcessor, 
            IUserPasswordUpdater userPasswordUpdater, 
            IDevelopersGetter developersGetter,
            IErrorsAssignedDeveloperUpdater errorsAssignedDeveloperUpdater, 
            IDeactivateError deactivateError, 
            IProjectsGetter projectsGetter)
        {
            _logger = logger;
            _activeErrorsGetter = activeErrorsGetter;
            _updateErrorStatusProcessor = updateErrorStatusProcessor;
            _devLogInCheckProcessor = devLogInCheckProcessor;
            _userPasswordUpdater = userPasswordUpdater;
            _developersGetter = developersGetter;
            _errorsAssignedDeveloperUpdater = errorsAssignedDeveloperUpdater;
            _deactivateError = deactivateError;
            _projectsGetter = projectsGetter;
        }

        [HttpGet("GetErrors")]
        public IActionResult GetErrors()
        {
            _logger.Log("GetAllErrors Called");
            try
            {
                return new OkObjectResult(_activeErrorsGetter.Get());
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed");
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpGet("GetProjects")]
        public IActionResult GetProjects()
        {
            _logger.Log("GetProjects Called");
            try
            {
                return new OkObjectResult(_projectsGetter.Get());
            }
            catch (Exception e)
            {
                _logger.Log($"GetProjects Failed");
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
        
        [HttpPut("DeactivateError")]
        public IActionResult DeactivateError([FromBody] DeactivateErrorModel model)
        {
            _logger.Log($"DeactivateError Called for ErrorId{model.ErrorId}");
            try
            {
                var error = _deactivateError.Deactivate(model.ErrorId);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"DeactivateError Failed for ErrorId{model.ErrorId}");

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
