using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labWork3.Models;

namespace labWork3.Core.Serializers
{
    internal interface ISerializer
    {
        IList<Contact> Deserialize();

        void Serialize(IList<Contact> contacts);
    }
}
