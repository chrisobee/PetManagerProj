﻿using PetManager.Contracts;
using PetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Data
{
    public class PetOwnerRepository : RepositoryBase<PetOwner>, IPetOwnerRepository
    {
        public PetOwnerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreatePetOwner(PetOwner petOwner) => Create(petOwner);
        public void DeletePetOwner(PetOwner petOwner) => Delete(petOwner);

        public void EditPetOwner(PetOwner petOwner) => Update(petOwner);
        public async Task<PetOwner> FindOwner(string userId)
        {
            var results = await FindByCondition(p => p.IdentityUserId == userId);
            var currentUser = results.SingleOrDefault();
            return currentUser;
        }

        public async Task<PetOwner> FindOwnerWithId(int? id)
        {
            var results = await FindByCondition(p => p.PetOwnerId == id);
            var petOwner = results.SingleOrDefault();
            return petOwner;
        }
        public async Task<int> FindOwnerId(string userId)
        {
            var results = await FindByCondition(p => p.IdentityUserId == userId);
            var currentUser = results.Select(p => p.PetOwnerId).FirstOrDefault();
            return currentUser;
        }

        public async Task<PetOwner> FindOwnerByEmail(string email)
        {
            var results = await FindByCondition(p => p.IdentityUser.Email == email);
            var person = results.SingleOrDefault();
            return person;
        }
    }
}
