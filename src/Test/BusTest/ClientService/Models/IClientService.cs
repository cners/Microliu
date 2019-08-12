using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientService.Models
{
    public interface IClientService
    {
        Client GetClientById(int _id);
    }
}
