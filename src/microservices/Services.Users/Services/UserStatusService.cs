using System;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Users.Domain;
using Services.Users.Factories;
using Services.Users.Repositories;

namespace Services.Users.Services
{
    public class UserStatusService : IUserStatusService
    {
        private readonly IVmsLogger<UserStatusService> logger;
        private readonly IUserStatusRepository repository;
        private readonly IAccessRecordFactory factory;

        public UserStatusService(IVmsLogger<UserStatusService> logger, IUserStatusRepository repository, IAccessRecordFactory factory)
        {
            this.logger = logger;
            this.repository = repository;
            this.factory = factory;
        }

        public async Task Update(Guid userId, AccessAction action, Guid siteId)
        {
            var status = await repository.GetStatusForUserAsync(userId);
            if(status is null)
            {
                logger.LogInformation("No status found creating one now for user: " + userId);
                status = factory.Create(userId, siteId, action);
                await repository.AddAsync(status);
                return;
            }

            logger.LogInformation($"Updated users status {userId} to: {action}");

            status.Update(action, siteId);
            await repository.UpdateAsync(status);
        }
    }
}
