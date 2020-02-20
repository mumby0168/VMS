using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Logging;
using Services.Common.Queries;
using Services.Users.Domain;
using Services.Users.Dtos;
using Services.Users.Factories;
using Services.Users.Queries;
using Services.Users.Repositories;

namespace Services.Users.Handlers.Queries
{
    public class GetLatestSiteAccessRecordsHandler : IQueryHandler<GetLatestSiteAccessRecords, IEnumerable<LatestAccessRecordDto>>
    {
        private readonly IVmsLogger<GetBusinessAccessRecordsHandler> _logger;
        private readonly IAccessRecordRepository _accessRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _statusRepository;

        public GetLatestSiteAccessRecordsHandler(IVmsLogger<GetBusinessAccessRecordsHandler> logger, IAccessRecordRepository accessRecordRepository, IUserRepository userRepository, IUserStatusRepository statusRepository)
        {
            _logger = logger;
            _accessRecordRepository = accessRecordRepository;
            _userRepository = userRepository;
            _statusRepository = statusRepository;
        }
        public async Task<IEnumerable<LatestAccessRecordDto>> HandleAsync(GetLatestSiteAccessRecords query)
        {

            IEnumerable<IUserStatus> states = await _statusRepository.GetForSiteAsync(query.SiteId);            

            var records = new List<LatestAccessRecordDto>();        
            

            foreach (var state in states)
            {
                var user = await _userRepository.GetAsync(state.UserId);

                if(user is null) 
                {
                    _logger.LogWarning($"User not found to match with current state id is: {state.UserId}");                    
                }

                records.Add(new LatestAccessRecordDto
                {
                    Action = state.CurrentState == AccessAction.In ? "in" : "out",
                    Id = state.Id,
                    TimeStamp = state.Updated,
                    UserId = state.UserId,
                    FullName = user?.FirstName + " " + user?.SecondName,
                    Email = user?.Email,
                    ContactNumber = user?.PhoneNumber,
                    Initials = $"{user?.FirstName[0]}{user?.SecondName[0]}",
                    Code = user?.Code.ToString()            
                });                                        
            }                        

            return records;
        }
    }
}
