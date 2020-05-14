using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Contracts
{
    public interface IContactsJxnRepository : IRepositoryBase<ContactsJxn>
    {
        void AddContact(int ownerId, int? contactId);
        Task<List<int?>> CheckForContacts(int ownerId);
    }
}
