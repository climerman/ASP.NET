using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Contacts
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactValue { get; set; }

        [ForeignKey(nameof(ContactType))]
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}