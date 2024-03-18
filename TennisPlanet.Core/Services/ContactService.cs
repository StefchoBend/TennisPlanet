using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string firstName, string email, string message)
        {
            Contact item = new Contact
            {
                FirstName = firstName,
                Email = email,
                Message = message
            };
            _context.Contacts.Add(item);
            return _context.SaveChanges() != 0;
        }

        public List<Contact> GetMessage()
        {
            return _context.Contacts.ToList();
        }

    }
}
