using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using DAL.Interfaces.Contacts;
using Domain.Contacts;

namespace DAL.Repositories.Contacts
{
    public class PersonRepository : EFRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}