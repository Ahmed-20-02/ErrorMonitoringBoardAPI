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
        private readonly ILogger _logger;
        
        
        public ErrorsController(IDbContextFactory<DataContext> contextFactory, 
            IAllErrorsGetter allErrorsGetter, 
            ILogger logger, 
            IUnresolvedErrorsGetter unresolvedErrorsGetter, 
            IErrorStatusUpdater errorStatusUpdater, 
            IUpdateErrorStatusProcessor updateErrorStatusProcessor)
        {
            _contextFactory = contextFactory;
            _allErrorsGetter = allErrorsGetter;
            _logger = logger;
            _unresolvedErrorsGetter = unresolvedErrorsGetter;
            _errorStatusUpdater = errorStatusUpdater;
            _updateErrorStatusProcessor = updateErrorStatusProcessor;
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
        public IActionResult UpdateSomethingWithPatch([FromBody] UpdateErrorStatusModel model)
        {
            _logger.Log($"GetAllErrors Called for ErrorId{model.ErrorId} updating to status {model.StatusId} ");
            try
            {
                var error = _updateErrorStatusProcessor.Process(model); //_errorStatusUpdater.Update(model.ErrorId, model.StatusId);
                return new OkObjectResult(error);
            }
            catch (Exception e)
            {
                _logger.Log($"GetAllErrors Failed for ErrorId{model.ErrorId} updating to status {model.StatusId} ");

                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
