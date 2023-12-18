using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using labWork3.Core;
using labWork3.Core.Serializers;
using labWork3.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace labWork3Tests.Core
{
    public class SerializeBackedContactRepositoryTest
        : BaseContactRepositoryTest<SerializeBackedContactRepository>,
        IDisposable
    {
        IList<Contact> mockSerializerList = new List<Contact>();
        ISerializer _serializer;
        public SerializeBackedContactRepositoryTest()
        {
            var mockSerializer = new Mock<ISerializer>();
            mockSerializer.Setup(m => m.Serialize(It.IsAny<IList<Contact>>()))
                .Callback<IList<Contact>>((s) => mockSerializerList = s);

            mockSerializer.Setup(m => m.Deserialize())
                .Returns(()=> (mockSerializerList ?? new List<Contact>()));

            _serializer = mockSerializer.Object;
            this.contactRepository = new SerializeBackedContactRepository(_serializer);
        }

        private void FillList(IList<Contact> list)
        {
            mockSerializerList = list;
        }


        [Fact]
        public void Save_Save2_Contacts_To_Data_Source()
        {
            var contact1 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };
            var contact2 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };

            contactRepository.AddContact(contact1);
            contactRepository.AddContact(contact2);

            var actual = mockSerializerList.Count;
            Assert.Equal(2, actual);
        }

        [Fact]
        public void Load_Load2_Contacts_From_Data_Source()
        {
            var contact1 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };
            var contact2 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };

            contactRepository.AddContact(contact1);
            contactRepository.AddContact(contact2);
            contactRepository = new(_serializer);
            
            var actual = contactRepository.GetAllContacts().Count;
            Assert.Equal(2, actual);
        }
    }
}
