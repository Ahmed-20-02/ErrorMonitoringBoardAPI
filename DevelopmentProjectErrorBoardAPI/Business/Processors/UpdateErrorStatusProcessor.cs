namespace DevelopmentProjectErrorBoardAPI.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Services;
    using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
    
    public class UpdateErrorStatusProcessor : IUpdateErrorStatusProcessor
    {
        private readonly IErrorStatusUpdater _errorStatusUpdater;
        private readonly IEmailService _emailService;
        private readonly IUserByIdGetter _userByIdGetter;

        public UpdateErrorStatusProcessor(IErrorStatusUpdater errorStatusUpdater, 
            IEmailService emailService, 
            IUserByIdGetter userByIdGetter)
        {
            _errorStatusUpdater = errorStatusUpdater;
            _emailService = emailService;
            _userByIdGetter = userByIdGetter;
        }

        public ErrorModel Process(UpdateErrorStatusModel model)
        {
           var error = _errorStatusUpdater.Update(model.ErrorId, model.StatusId);

           if(model.CustomerId != null && model.AgentId != 1)
           {     var agent = _userByIdGetter.Get(model.AgentId);
               var dev = _userByIdGetter.Get(model.DevId);
               
               _emailService.SendEmail(agent.Result, dev.Result, model.CustomerId, model.StatusId);
           }
      
           return error;
        }
    }
}