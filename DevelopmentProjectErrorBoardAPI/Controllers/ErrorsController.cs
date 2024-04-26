using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Data;
using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
using DevelopmentProjectErrorBoardAPI.Resources;
using DevelopmentProjectErrorBoardAPI.Resources.Requests;
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
        private readonly IErrorCloser _errorCloser;
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
            IProjectsGetter projectsGetter, 
            IErrorCloser errorCloser)
        {
            _logger = logger;
            _activeErrorsGetter = activeErrorsGetter;
            _updateErrorStatusProcessor = updateErrorStatusProcessor;
            _devLogInCheckProcessor = devLogInCheckProcessor;
            _userPasswordUpdater = userPasswordUpdater;
            _developersGetter = developersGetter;
            _errorsAssignedDeveloperUpdater = errorsAssignedDeveloperUpdater;
            _projectsGetter = projectsGetter;
            _errorCloser = errorCloser;
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
        public async Task<IActionResult> UpdateErrorStatus([FromBody] UpdateErrorStatusRequest request)
        {
            _logger.Log($"GetAllErrors Called for ErrorId{request.ErrorId} updating to status {request.StatusId} ");
            try
            {
                var error = await _updateErrorStatusProcessor.Process(request);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed for ErrorId{request.ErrorId} updating to status {request.StatusId} ");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("DeactivateError")]
        public async Task<IActionResult> DeactivateError([FromBody] DeactivateErrorRequest request)
        {
            _logger.Log($"DeactivateError Called for ErrorId{request.ErrorId}");
            try
            {
                var error = _errorCloser.Close(request.ErrorId);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"DeactivateError Failed for ErrorId{request.ErrorId}");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("UpdateErrorsAssignedDeveloper")]
        public async Task<IActionResult> UpdateErrorsAssignedDeveloper([FromBody] UpdateErrorsAssignedDeveloperRequest request)
        {
            _logger.Log($"UpdateErrorsAssignedDeveloper Called for ErrorId{request.ErrorId} updating to dev {request.DevId} ");
            try
            {
                var error = _errorsAssignedDeveloperUpdater.Update(request);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"UpdateErrorsAssignedDeveloper Failed for ErrorId{request.ErrorId} updating to dev {request.DevId} ");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("DevLogIn")]
        public async Task<IActionResult> DevLogIn([FromBody] LogInRequest request)
        {
            _logger.Log($"LogIn Called for user email {request.EmailAddress}");
            try
            {
                var logInModel = await _devLogInCheckProcessor.Process(request);
                return new OkObjectResult(logInModel);
            }
            catch (Exception e)
            {
                _logger.Log($"LogIn Failed for user email {request.EmailAddress}");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("CreateError")]
        public async Task<IActionResult> CreateError([FromBody] CreateErrorRequest model)
        {
            _logger.Log($"CreateError Called");
            try
            {
                //var logInModel = await _devLogInCheckProcessor.Process(model);
                return new OkObjectResult(6/*logInModel*/);
            }
            catch (Exception e)
            {
                _logger.Log($"CreateError Failed");

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
