using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Contracts
{
    public interface IContactService
    {
        bool Create(string firstName, string email, string message);  
        List<Contact> GetMessage();
    }
}
