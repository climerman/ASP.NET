using System.Collections.Generic;
using Domain.Contacts;

namespace DAL.Interfaces.Contacts
{
    public interface IContactRepository : IEFRepository<Contact>
    {
        List<Contact> GetContactsByPersonId(int personId);
    }
}