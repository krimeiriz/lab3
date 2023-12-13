using labWork3.Core.Serializers;
using labWork3.DB;
using labWork3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork3.Core
{
    public class DBBackedContactRepository : ContactRepository
    {
        private RepositoryDBContext _context;
        public DBBackedContactRepository(RepositoryDBContext context)
            : base(context.Contacts.ToList())
        {
            this._context = context;
        }

        public override void AddContact(Contact contact)
        {
            base.AddContact(contact);
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }
        public override void ResetRepository()
        {
            base.ResetRepository();
            _context.Database.ExecuteSqlRaw("delete from Contacts");
        }
    }
}
