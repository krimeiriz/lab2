﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork2
{
    internal class ContactRepository
    {
        private static ContactRepository Instance;

        internal static int currentId;
        private static Dictionary<int, Contact> Contacts = new Dictionary<int, Contact>();

        private ContactRepository() { }

        public ContactRepository getInstance()
        {
            if (Instance == null)
                Instance = new ContactRepository();
           
            return Instance;
        }

        public void AddContact(Contact contact)
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

        public List<Contact>FindByFirstName(string firstname)
        {
            return FindContactsByPredicate(c => 
            {
                var cLower = c.FirstName.ToLower();
                return cLower.Contains(firstname.ToLower());
            });
        }


        public List<Contact> FindByLastName(string lastname)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.LastName.ToLower();
                return cLower.Contains(lastname.ToLower());
            });
        }

        public List<Contact> FindByFullName(string firstName, string lastname)
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
    }

    struct Contact{
        public int Id { private set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Contact(string firstName, string lastName, string phoneNumber, string email)
        {
            Id = ContactRepository.currentId++;
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
    }
}
