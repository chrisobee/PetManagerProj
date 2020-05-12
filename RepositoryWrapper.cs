using PetManager.Contracts;
using PetManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IPetOwnerRepository _petOwner;
        private IPetRepository _pet;
        private IToDoTasksRepository _toDoTask;
        private IPetOwnershipRepository _petOwnership;
        private IAnimalTypeRepository _animalType;
        public IPetRepository Pet
        {
            get
            {
                if(_pet == null)
                {
                    _pet = new PetRepository(_context);
                }
                return _pet;
            }
        }
        public IPetOwnerRepository PetOwner
        {
            get
            {
                if(_petOwner == null)
                {
                    _petOwner = new PetOwnerRepository(_context);
                }
                return _petOwner;
            }
        }
        public IToDoTasksRepository ToDoTask
        {
            get
            {
                if(_toDoTask == null)
                {
                    _toDoTask = new ToDoTasksRepository(_context);
                }
                return _toDoTask;
            }
        }
        public IPetOwnershipRepository PetOwnership
        {
            get
            {
                if(_petOwnership == null)
                {
                    _petOwnership = new PetOwnershipRepository(_context);
                }
                return _petOwnership;
            }
        }
        public IAnimalTypeRepository AnimalType
        {
            get
            {
                if(_animalType == null)
                {
                    _animalType = new AnimalTypeRepository(_context);
                }
                return _animalType;
            }
        }
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
