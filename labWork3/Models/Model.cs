using labWork3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork3.Models
{
    public class Contact
    {
        public int Id { set; get; } = 0!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        /*
        public Contact(string firstName, string lastName, string phoneNumber, string email)
        {
            ContactRepository.currentId++;
            Id = ContactRepository.currentId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;

        }

        public Contact(int id, string firstName, string lastName, string phoneNumber, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        */
        public override string ToString()
        {
            return "#" + Id + " Name:" + FirstName + "\n" +
                "Lastname: " + LastName + "\n" +
                "Phone number: " + PhoneNumber + "\n" +
                "E-mail: " + Email + "\n";
        }
    }
}
