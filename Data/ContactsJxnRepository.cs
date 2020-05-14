using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class ContactsJxnRepository : RepositoryBase<ContactsJxn>, IContactsJxnRepository
    {
        public ContactsJxnRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void AddContact(int ownerId, int? contactId)
        {
            ContactsJxn contactRecord = new ContactsJxn()
            {
                PetOwnerId = ownerId,
                ContactId = contactId
            };
            Create(contactRecord);
        }

        public async Task<List<int?>> CheckForContacts(int ownerId)
        {
            var results = await FindByCondition(c => c.PetOwnerId == ownerId);
            var ownersContacts = results.Select(c => c.ContactId).ToList();
            return ownersContacts;
        }
    }
}
