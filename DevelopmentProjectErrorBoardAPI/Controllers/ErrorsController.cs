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
        public async Task<IActionResult> GetErrors()
        {
            _logger.Log("GetAllErrors Called");
            try
            {
                var errors = await _activeErrorsGetter.Get();
                return new OkObjectResult(errors);
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed");
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetProjects()
        {
            _logger.Log("GetProjects Called");
            try
            {
                var projects = await _projectsGetter.Get();
                return new OkObjectResult(projects);
            }
            catch (Exception e)
            {
                _logger.Log($"GetProjects Failed");
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpGet("GetDevelopers")]
        public async Task<IActionResult> GetDevelopers()
        {
            _logger.Log($"GetDevelopers Called");
            try
            {
                var developers = await _developersGetter.Get();
                return new OkObjectResult(developers);
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed");
                return new BadRequestObjectResult(e.Message);
            }
        }
        
        [HttpPut("UpdateErrorStatus")]
        public async Task<IActionResult> UpdateErrorStatus([FromBody] UpdateErrorStatusModel model)
        {
            _logger.Log($"GetAllErrors Called for ErrorId{model.ErrorId} updating to status {model.StatusId} ");
            try
            {
                var error = await _updateErrorStatusProcessor.Process(model);
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
        public async Task<IActionResult> DeactivateError([FromBody] DeactivateErrorModel model)
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
        public async Task<IActionResult> UpdateErrorsAssignedDeveloper([FromBody] UpdateErrorsAssignedDeveloperModel model)
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
        public async Task<IActionResult> DevLogIn([FromBody] LogInModel model)
        {
            _logger.Log($"LogIn Called for user email {model.EmailAddress}");
            try
            {
                var logInModel = await _devLogInCheckProcessor.Process(model);
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
        public async Task<IActionResult> TestingUpdatePassword(int userId, string password)
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
