using labWork3.Core.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork3.Core
{
    internal class SerializeBackedContactRepository : ContactRepository
    {
        private ISerializer _serializer;
        public SerializeBackedContactRepository(ISerializer serializer)
            : base(serializer.Deserialize())
        {
            this._serializer = serializer;
        }

        public override void AddContact(Contact contact)
        {
            base.AddContact(contact);
            Save();
        }
        public override void ResetRepository()
        {
            base.ResetRepository();
            Save();
        }

        private void Save()
        {
            _serializer.Serialize(base.Contacts.Values.ToList());
        }
    }
}
