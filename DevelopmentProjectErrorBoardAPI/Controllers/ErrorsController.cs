namespace DevelopmentProjectErrorBoardAPI.Controllers
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using Microsoft.AspNetCore.Mvc;
    using ILogger = Logger.ILogger;
    
    [ApiController]
    [Route("[controller]")]
    public class ErrorsController : ControllerBase
    {
        private readonly IActiveErrorsGetter _activeErrorsGetter;
        private readonly IDevelopersGetter _developersGetter;
        private readonly IProjectsGetter _projectsGetter;
        private readonly IErrorsAssignedDeveloperUpdater _errorsAssignedDeveloperUpdater;
        private readonly IUpdateErrorStatusProcessor _updateErrorStatusProcessor;
        private readonly IErrorCloser _errorCloser;
        private readonly IDevLogInCheckProcessor _devLogInCheckProcessor;
        private readonly ICreateErrorProcessor _createErrorProcessor;
        private readonly ILogger _logger;
        
        public ErrorsController(
            ILogger logger, 
            IActiveErrorsGetter activeErrorsGetter, 
            IUpdateErrorStatusProcessor updateErrorStatusProcessor, 
            IDevLogInCheckProcessor devLogInCheckProcessor, 
            IDevelopersGetter developersGetter,
            IErrorsAssignedDeveloperUpdater errorsAssignedDeveloperUpdater, 
            IProjectsGetter projectsGetter, 
            IErrorCloser errorCloser,
            ICreateErrorProcessor createErrorProcessor)
        {
            _logger = logger;
            _activeErrorsGetter = activeErrorsGetter;
            _updateErrorStatusProcessor = updateErrorStatusProcessor;
            _devLogInCheckProcessor = devLogInCheckProcessor;
            _developersGetter = developersGetter;
            _errorsAssignedDeveloperUpdater = errorsAssignedDeveloperUpdater;
            _projectsGetter = projectsGetter;
            _errorCloser = errorCloser;
            _createErrorProcessor = createErrorProcessor;
        }

        [HttpGet("HealthCheck")]
        public async Task<IActionResult> HealthCheck()
        {
            _logger.Log("GetAllErrors Called");
            try
            {
                return new OkObjectResult("I AM HEALTHY");
            }
            catch (Exception e)
            {
                _logger.Log($"Health check Failed {e}");
                return new BadRequestObjectResult(e.Message);
            }
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
            _logger.Log($"UpdateErrorStatus called for ErrorId {request.ErrorId} updating to status {request.StatusId} ");
            try
            {
                var error = await _updateErrorStatusProcessor.Process(request);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"UpdateErrorStatus failed for ErrorId {request.ErrorId} updating to status {request.StatusId} ");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("DeactivateError")]
        public async Task<IActionResult> DeactivateError([FromBody] DeactivateErrorRequest request)
        {
            _logger.Log($"DeactivateError called for ErrorId {request.ErrorId}");
            try
            {
                var error = await _errorCloser.Close(request.ErrorId);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"DeactivateError failed for ErrorId {request.ErrorId}");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("UpdateErrorsAssignedDeveloper")]
        public async Task<IActionResult> UpdateErrorsAssignedDeveloper([FromBody] UpdateErrorsAssignedDeveloperRequest request)
        {
            _logger.Log($"UpdateErrorsAssignedDeveloper called for ErrorId {request.ErrorId} updating to developer {request.DevId} ");
            try
            {
                var error = await _errorsAssignedDeveloperUpdater.Update(request);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"UpdateErrorsAssignedDeveloper failed for ErrorId {request.ErrorId} updating to developer {request.DevId} ");

                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("DevLogIn")]
        public async Task<IActionResult> DevLogIn([FromBody] LogInRequest request)
        {
            _logger.Log($"LogIn called for user email {request.EmailAddress}");
            try
            {
                var logInModel = await _devLogInCheckProcessor.Process(request);
                return new OkObjectResult(logInModel);
            }
            catch (Exception e)
            {
                _logger.Log($"LogIn failed for user email {request.EmailAddress}");

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
              var result = await _createErrorProcessor.Process(model);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.Log($"CreateError Failed");

                Console.WriteLine(e);
                throw;
            }
        }
    }
}
