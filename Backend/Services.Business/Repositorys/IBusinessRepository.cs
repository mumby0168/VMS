using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Businesses.Domain;

namespace Services.Businesses.Repositorys
{
    public interface IBusinessRepository
    {
        Task Add(Business business);
    }
}
