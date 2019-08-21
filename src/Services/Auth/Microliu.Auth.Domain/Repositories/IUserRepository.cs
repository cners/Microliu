﻿using Microliu.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microliu.Auth.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IQueryable<User> Query(string id, string positionId);
    }
}