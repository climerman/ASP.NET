using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Contacts
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual List<Contact> Contacts { get; set; }

        [NotMapped]
        public string FirstLastName => Firstname + " " + Lastname;
        [NotMapped]
        public string LastFirstName => Lastname + " " + Firstname;
    }
}