using System;
using NUnit.Framework;
using labWork2;
using System.Runtime.InteropServices;
using static NUnit.Framework.Constraints.Tolerance;

namespace LabWork2Tests
{
    [TestFixture]
    public class ContactRepositoryTest
    {
        readonly ContactRepository contactRepository = ContactRepository.GetInstance();
        
        [SetUp]
        public void Reset()
        {
            contactRepository.ResetRepository();
        }


        [Test]
        public void AddContact_Add1Contact()
        {
            var contact1 = new Contact("test", "test", "777", "test");
            contactRepository.AddContact(contact1);
           
            var expected = 1;
            var actual = contactRepository.GetAllContacts().Count;

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void GetAllContacts_Get2Contacts()
        {
            var contact1 = new Contact("test", "test", "777", "test");
            contactRepository.AddContact(contact1);
            var contact2= new Contact("test", "test", "777", "test");
            contactRepository.AddContact(contact2);

            var expected = 2;
            var actual = contactRepository.GetAllContacts().Count;

            Assert.That(expected, Is.EqualTo(actual));
        }


        [TestCase(new string[] {"Alex","John"}, "alex", 1)]
        [TestCase(new string[] { "Alex", "John" }, "ALEX", 1)]
        [TestCase(new string[] { "Alex", "John", "Alexey" }, "Alex", 2)]
        [TestCase(new string[] { "Alex", "John" }, "al", 1)]
        [TestCase(new string[] { }, "alex", 0)]
        public void FindByFirstname(
            string[] names,
            string searchMask,
            int resultSize
            )
        {
            foreach(string name in names)
            {
                Contact c = new Contact(name, "test", "111", "test");
                contactRepository.AddContact(c);
            }

            var actual = contactRepository.FindByFirstname(searchMask).Count;

            Assert.That(resultSize, Is.EqualTo(actual));
        }

        [TestCase(new string[] { "Smith", "Li" }, "smith", 1)]
        [TestCase(new string[] { "smith", "li" }, "SMITH", 1)]
        [TestCase(new string[] { "Smith", "Li", "Linden" }, "li", 2)]
        [TestCase(new string[] { "Smith", "Li" }, "sm", 1)]
        [TestCase(new string[] { }, "sm", 0)]
        public void FindByLastname(
            string[] range,
            string searchMask,
            int resultSize
            )
        {
            foreach (string lastname in range)
            {
                Contact c = new Contact("test", lastname, "111", "test");
                contactRepository.AddContact(c);
            }

            var actual = contactRepository.FindByLastname(searchMask).Count;

            Assert.That(resultSize, Is.EqualTo(actual));
        }

        [TestCase(new string[] { "Alex Johnson", "Li Sharper" }, "alex johnson", 1)]
        [TestCase(new string[] { "alex johnson", "li Sharper" }, "ALEX JOHNSON", 1)]
        [TestCase(new string[] { "Alex Johnson", "Li Sharper", "Alexey Johnson" }, "Alex Johnson", 2)]
        [TestCase(new string[] { "Alex Johnson", "Li Sharper" }, "al jo", 1)]
        [TestCase(new string[] { }, "alex johnson", 0)]
        public void FindByFullname(
            string[] fullnameRange,
            string searchMask,
            int resultSize
        )
        {
            foreach (string name in fullnameRange)
            {
                string[] tokensName = name.Split(new char[] { ' ' });
                Contact c = new Contact(tokensName[0], tokensName[1], "111", "test");
                contactRepository.AddContact(c);
            }

            var tokens = searchMask.Split(new char[]{' '});
            var actual = contactRepository.FindByFullname(tokens[0], tokens[1]).Count;

            Assert.That(resultSize, Is.EqualTo(actual));
        }

        [TestCase(new string[] { "755633", "112233" }, "755633", 1)]
        [TestCase(new string[] { "755633", "112233", "7556331" }, "755633", 2)]
        [TestCase(new string[] { "755633", "112233"}, "755", 1)]
        [TestCase(new string[] { }, "755633", 0)]
        public void FindByPhonenumber(
           string[] range,
           string searchMask,
           int resultSize
           )
        {
            foreach (string number in range)
            {
                Contact c = new Contact("test", "test", number, "test");
                contactRepository.AddContact(c);
            }

            var actual = contactRepository.FindByPhoneNumber(searchMask).Count;

            Assert.That(resultSize, Is.EqualTo(actual));
        }

        [TestCase(new string[] { "sm@z.com", "l@mail.com" }, "sm@z.com", 1)]
        [TestCase(new string[] { "sm@z.com", "l@mail.com" }, "sm", 1)]
        [TestCase(new string[] { "sm@z.com", "l@mail.com", "ksm@z.com" }, "sm@z.com", 2)]
        [TestCase(new string[] { "sm@z.com", "l@mail.com" }, "sm", 1)]
        [TestCase(new string[] { }, "sm", 0)]
        public void FindByEmail(
            string[] range,
            string searchMask,
            int resultSize
        )
        {
            foreach (string email in range)
            {
                Contact c = new Contact("test", "test", "111", email);
                contactRepository.AddContact(c);
            }

            var actual = contactRepository.FindByEmail(searchMask).Count;

            Assert.That(resultSize, Is.EqualTo(actual));
        }

        [TestCase(
           new string[]
                {
                     "Alex Morgan 777 email1",
                     "Li Sharper 666 email2"
                },
           "777",
           1
       )]
        [TestCase(
           new string[]
                {
                     "Alex Morgan 777 email1",
                     "Li Sharper 666 email2"
                },
           "SHARPER",
           1
       )]
        [TestCase(
           new string[]
                {
                     "Alex Morgan 777 email1",
                     "Li Sharper 666 email2"
                },
           "EMAIL",
           2
       )]
        [TestCase(
           new string[] { },
           "777",
           0
       )]
        public void FindByWholeFields(
            string[] contactsRange,
            string searchMask,
            int resultSize
        )
        {
            foreach(string name in contactsRange)
            {
                string[] tokensContact = name.Split(new char[] { ' ' });
                Contact c = new Contact
                (
                    tokensContact[0], 
                    tokensContact[1], 
                    tokensContact[2], 
                    tokensContact[3]
                );
                contactRepository.AddContact(c);
            }

            var actual = contactRepository.FindByAnyField(searchMask).Count;

            Assert.That(resultSize, Is.EqualTo(actual));
        }


    }
}
