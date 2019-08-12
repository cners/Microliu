using ClientService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientService.Services
{
    public interface IClientRepository : IRepository<Client, ClientDbContext>
    {
        Client GetClientById(int _personId);
    }
}
