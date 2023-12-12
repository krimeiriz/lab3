﻿using labWork3.Core.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace labWork3.Core
{
    public abstract class ContactRepository
    { 
        internal static int currentId = 0;
        protected readonly Dictionary<int, Contact> Contacts = new Dictionary<int, Contact>();

        protected ContactRepository(IList<Contact> contacts) {
            foreach(Contact contact in contacts)
            {
                Contacts.Add(contact.Id, contact);
            }
            currentId = contacts.DefaultIfEmpty().Max(c => c.Id);
        }
        public static ContactRepository CreateRepository(RepositoryType type, string? path)
        {
            ISerializer serializer;
            switch (type)
            {
                case RepositoryType.JSON:
                    serializer = new JSONSerializer(path);
                    return new SerializeBackedContactRepository(serializer);
                case RepositoryType.XML:
                    serializer = new XMLSerializer(path);
                    return new SerializeBackedContactRepository(serializer);
                default:
                    throw new ArgumentException();
            }
        }

        public virtual void AddContact(Contact contact)
        {
            Contacts.Add(contact.Id, contact);
        }

        public List<Contact> GetAllContacts()
        {
            return Contacts.Values.ToList();
        }

        private List<Contact> FindContactsByPredicate(Predicate<Contact> predicate)
        {
            List<Contact> resultSet = new List<Contact>();
            foreach (Contact contact in Contacts.Values)
            {
                if (predicate(contact))
                {
                    resultSet.Add(contact);
                };
            }
            return resultSet;
        }

        public List<Contact> FindByFirstname(string firstname)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.FirstName.ToLower();
                return cLower.Contains(firstname.ToLower());
            });
        }


        public List<Contact> FindByLastname(string lastname)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.LastName.ToLower();
                return cLower.Contains(lastname.ToLower());
            });
        }

        public List<Contact> FindByFullname(string firstName, string lastname)
        {
            return FindContactsByPredicate(c =>
            {
                var fLower = c.FirstName.ToLower();
                var lLower = c.LastName.ToLower();
                return fLower.Contains(firstName.ToLower())
                        && lLower.Contains(lastname.ToLower());
            });
        }

        public List<Contact> FindByPhoneNumber(string phoneNumber)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.PhoneNumber.ToLower();
                return cLower.Contains(phoneNumber.ToLower());
            });
        }

        public List<Contact> FindByEmail(string email)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.Email.ToLower();
                return cLower.Contains(email.ToLower());
            });
        }

        public List<Contact> FindByAnyField(string field)
        {
            return FindContactsByPredicate(c =>
            {
                var fLower = c.FirstName.ToLower();
                var lLower = c.LastName.ToLower();
                var pLower = c.PhoneNumber.ToLower();
                var eLower = c.Email.ToLower();
                var fieldLower = field.ToLower();
                return fLower.Contains(fieldLower)
                        || lLower.Contains(fieldLower)
                        || pLower.Contains(fieldLower)
                        || eLower.Contains(fieldLower);
            });
        }

        public virtual void ResetRepository()
        {
            currentId = 1;
            Contacts.Clear();
        }
    }

    public struct Contact
    {
        public int Id { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

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

        public override string ToString()
        {
            return "#" + Id + " Name:" + FirstName + "\n" +
                "Lastname: " + LastName + "\n" +
                "Phone number: " + PhoneNumber + "\n" +
                "E-mail: " + Email + "\n";
        }
    }
}