using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Users.Dtos;
using Services.Users.Queries;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Queries
{
    public class GetUserInfoHandler : IQueryHandler<GetUserInfo, UserInfoDto>
    {
        private readonly IVmsLogger<GetUserInfoHandler> _logger;
        private readonly IUserRepository _repository;

        public GetUserInfoHandler(IVmsLogger<GetUserInfoHandler> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<UserInfoDto> HandleAsync(GetUserInfo query)
        {
            var user = await _repository.GetFromAccountId(query.AccountId);
            if (user is null)
            {
                throw new VmsException(Codes.InvalidId,
                    $"The user with account id: {query.AccountId} can not be found.");
            }

            return new UserInfoDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                BasedSiteId = user.BasedSiteId,
                Code = user.Code.ToString()
            };
        }
    }
}
