using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Logs.Domain;

namespace Services.Logs.Repositorys
{
    public interface ILogsRepository
    {
        Task<IEnumerable<ILog>> GetAllAsync();

        Task Purge();
    }
}
