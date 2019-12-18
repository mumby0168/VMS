﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Mongo;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;

        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(IUser user)
        {
            return _repository.AddAsync(user as User);
        }

        public async Task<IUser> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            return user;
        }
    }
}
