﻿using Microliu.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microliu.Auth.DataOracle
{
    public class RoleRepository :BaseRepository, IRoleRepository
    {
        private readonly AuthContext _context;

        public RoleRepository(AuthContext context)
        {
            _context = context;
        }


        public async Task<Role> AddAsync(Role newEntity, CancellationToken ct = default)
        {
            await _context.role.AddAsync(newEntity);
            //await _context.SaveChangesAsync();
            return newEntity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}