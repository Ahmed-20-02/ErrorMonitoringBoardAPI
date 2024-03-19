using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
using DevelopmentProjectErrorBoardAPI.Data;
using DevelopmentProjectErrorBoardAPI.Data.Entities;
using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;

namespace DevelopmentProjectErrorBoardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetErrorsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IAllErrorsGetter _allErrorsGetter;
        private readonly IUnresolvedErrorsGetter _unresolvedErrorsGetter;
        private readonly ILogger _logger;
        
        public GetErrorsController(IDbContextFactory<DataContext> contextFactory, 
            IAllErrorsGetter allErrorsGetter, 
            ILogger logger, 
            IUnresolvedErrorsGetter unresolvedErrorsGetter)
        {
            _contextFactory = contextFactory;
            _allErrorsGetter = allErrorsGetter;
            _logger = logger;
            _unresolvedErrorsGetter = unresolvedErrorsGetter;
        }

        [HttpGet(Name = "GetAllErrors")]
        public IEnumerable<Error> Get()
        {
            _logger.Log("GetAllErrors Called");
            return _unresolvedErrorsGetter.Get(); //_allErrorsGetter.Get().ToList();
        }
    }
}
