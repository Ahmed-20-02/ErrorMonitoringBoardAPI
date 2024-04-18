using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Data;
using DevelopmentProjectErrorBoardAPI.Data.Entities;
using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
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
        private readonly IErrorStatusUpdater _errorStatusUpdater;
        private readonly IUpdateErrorStatusProcessor _updateErrorStatusProcessor;
        private readonly IDevLogInCheckProcessor _devLogInCheckProcessor;
        private readonly ILogger _logger;
        
        public ErrorsController(IDbContextFactory<DataContext> contextFactory, 
            IAllErrorsGetter allErrorsGetter, 
            ILogger logger, 
            IUnresolvedErrorsGetter unresolvedErrorsGetter, 
            IErrorStatusUpdater errorStatusUpdater, 
            IUpdateErrorStatusProcessor updateErrorStatusProcessor, 
            IDevLogInCheckProcessor devLogInCheckProcessor)
        {
            _contextFactory = contextFactory;
            _allErrorsGetter = allErrorsGetter;
            _logger = logger;
            _unresolvedErrorsGetter = unresolvedErrorsGetter;
            _errorStatusUpdater = errorStatusUpdater;
            _updateErrorStatusProcessor = updateErrorStatusProcessor;
            _devLogInCheckProcessor = devLogInCheckProcessor;
        }

        [HttpGet("GetAllErrors")]
        public IActionResult Get()
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
    }
}
