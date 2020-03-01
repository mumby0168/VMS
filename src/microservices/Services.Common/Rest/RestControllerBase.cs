using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Serializers;
using Services.Common.Domain;
using Services.Common.Mongo;

namespace Services.Common.Rest
{
    public abstract class RestControllerBase<TDomain, TDto> : ControllerBase where TDomain : IDomain where TDto : class
    {
        protected readonly IMongoRepository<TDomain> Repository;
        protected readonly IMapper Mapper;

        protected RestControllerBase(IMongoRepository<TDomain> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDomain>> Get(Guid id)
        {
            var result = await Repository.GetAsync(id);
            if (result == null) return NoContent();
            var dto = Mapper.Map<TDto>(result);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TDomain>>> GetAll()
        {
            var results = await Repository.GetAllAsync();
            var enumerable = results.ToList();
            if (!enumerable.Any())
                return NoContent();
    
            var dtos = new List<TDto>();

            foreach (var result in enumerable)
            {
                dtos.Add(Mapper.Map<TDto>(result));
            }
            
            
            return Ok(dtos);
        }


    }
}
