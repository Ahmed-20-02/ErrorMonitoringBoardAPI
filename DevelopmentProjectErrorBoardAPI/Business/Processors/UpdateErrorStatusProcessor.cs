
namespace DevelopmentProjectErrorBoardAPI.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;

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

        public async Task<ErrorModel> Process(UpdateErrorStatusRequest request)
        {
            try
            {
                var error = await _errorStatusUpdater.Update(request.ErrorId, request.StatusId);

                if (request.AgentId != (int)RolesEnum.System)
                {
                    var agent = await _userByIdGetter.Get(request.AgentId);
                    var dev = await _userByIdGetter.Get(request.DevId);

                    _emailService.SendEmail(agent, dev, request.CustomerId, request.StatusId, request.ErrorId);
                }

                return error;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}