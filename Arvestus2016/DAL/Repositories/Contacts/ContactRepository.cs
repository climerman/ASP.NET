using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using DAL.Interfaces.Contacts;
using Domain.Contacts;

namespace DAL.Repositories.Contacts
{
    public class ContactRepository : EFRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Contact> GetContactsByPersonId(int personId)
        {
            return DbSet.Where(c => c.PersonId == personId)
                .OrderBy(c => c.ContactTypeId)
                .ThenBy(c => c.ContactValue)
                .Include(t => t.ContactType)
                .Include(p => p.Person)
                .ToList();
        }
    }
}