using System.Collections.Generic;

namespace Domain.Contacts
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public string ContactTypeDescription { get; set; }

        public virtual List<Contact> Contacts { get; set; }
    }
}