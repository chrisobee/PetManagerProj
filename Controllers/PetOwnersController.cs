using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetManager.Contracts;
using PetManager.Data;
using PetManager.Models;
using PetManager.Services;
using PetManager.ViewModels;

namespace PetManager.Controllers
{
    public class PetOwnersController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IGoogleAPIs _googleAPI;
        public PetOwnersController(IRepositoryWrapper repo, IGoogleAPIs googleAPI)
        {
            _repo = repo;
            _googleAPI = googleAPI;
        }

        // GET: PetOwners
        public async Task<IActionResult> Index()
        {
            //Get userId and instantiate View Model
            TasksAndPetsVM tasksAndPets = new TasksAndPetsVM();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Find owner and set property on View Model
            var owner = await _repo.PetOwner.FindOwner(userId);
            if(owner == null)
            {
                return RedirectToAction("Create");
            }
            tasksAndPets.PetOwner = owner;

            //Find all of the owner's pets and set prop on View Model
            var petIds = await _repo.PetOwnership.FindAllPets(owner.PetOwnerId);
            tasksAndPets.CurrentUsersPets = await FindOwnersPets(petIds);

            tasksAndPets.CurrentUsersPets = await SetPetsAnimalTypes(tasksAndPets.CurrentUsersPets);

            //Find all tasks and set prop on View Model
            tasksAndPets.CurrentUsersTasks = await FindOwnersTasks(tasksAndPets.CurrentUsersPets);
            tasksAndPets.CurrentUsersTasks = FilterTasks(tasksAndPets.CurrentUsersTasks);

            //Find pet stores
            tasksAndPets.NearbyPetStores = await ShowNearbyPetStores(owner.PetOwnerId);

            //Find vets
            tasksAndPets.NearbyVets = await ShowNearbyVets(owner.PetOwnerId);

            return View(tasksAndPets);
        }

        //GET: PetOwners/Search
        public async Task<IActionResult> Search()
        {
            return View();
        }

        //POST: PetOwners/Search/(email)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string email)
        {

        }

        public async Task<List<ToDoTask>> FindOwnersTasks(List<Pet> pets)
        {
            List<ToDoTask> tasks = new List<ToDoTask>();
            foreach(Pet pet in pets)
            {
                var results = await _repo.ToDoTask.GetTasksByPet(pet.PetId);
                tasks.AddRange(results.ToList());
            }
            return tasks;
        }
        public List<ToDoTask> FilterTasks(List<ToDoTask> allToDoTasks)
        {
            List<ToDoTask> filteredList = new List<ToDoTask>();
            foreach (ToDoTask task in allToDoTasks)
            {
                if (task.TaskCompleted == false || task.ResetDay != DateTime.Today.Day)
                {
                    filteredList.Add(task);
                }
            }
            return filteredList;
        }
        public async Task<List<Pet>> FindOwnersPets(List<int> petIds)
        {
            List<Pet> ownersPets = new List<Pet>();
            foreach(int id in petIds)
            {
                var results = await _repo.Pet.FindByCondition(p => p.PetId == id);
                ownersPets.Add(results.FirstOrDefault());
            }
            return ownersPets;
        }

        public async Task<List<Pet>> SetPetsAnimalTypes(List<Pet> pets)
        {
            foreach (Pet pet in pets)
            {
                pet.AnimalType = await _repo.AnimalType.GetAnimalTypeById(pet.AnimalTypeId);
            }
            return pets;
        }

        // GET: PetOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petOwner = await _repo.PetOwner.FindOwnerWithId(id);
            if (petOwner == null)
            {
                return NotFound();
            }

            return View(petOwner);
        }

        // GET: PetOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetOwner petOwner)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                petOwner.IdentityUserId = userId;
                petOwner = await _googleAPI.GetOwnersCoordinates(petOwner);

                _repo.PetOwner.CreatePetOwner(petOwner);
                await _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(petOwner);
        }

        // GET: PetOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petOwner = await _repo.PetOwner.FindOwnerWithId(id);
            if (petOwner == null)
            {
                return NotFound();
            }
            return View(petOwner);
        }

        // POST: PetOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PetOwner petOwner)
        {
            if (id != petOwner.PetOwnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.PetOwner.EditPetOwner(petOwner);
                    await _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetOwnerExists(petOwner.PetOwnerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(petOwner);
        }

        // GET: PetOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petOwner = await _repo.PetOwner.FindOwnerWithId(id);
            if (petOwner == null)
            {
                return NotFound();
            }

            return View(petOwner);
        }

        // POST: PetOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petOwner = await _repo.PetOwner.FindOwnerWithId(id);
            _repo.PetOwner.DeletePetOwner(petOwner);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        public async Task<NearbyPlace> ShowNearbyVets(int id)
        {
            var petOwner = await _repo.PetOwner.FindOwnerWithId(id);
            NearbyPlace nearbyVets = await _googleAPI.GetNearbyVets(petOwner);
            nearbyVets = _googleAPI.PareDownList(nearbyVets);
            return nearbyVets;
        }

        public async Task<NearbyPlace> ShowNearbyPetStores(int id)
        {
            var petOwner = await _repo.PetOwner.FindOwnerWithId(id);
            NearbyPlace nearbyStores = await _googleAPI.GetNearbyPetStores(petOwner);
            nearbyStores = _googleAPI.PareDownList(nearbyStores);
            return nearbyStores;
        }

        
        private bool PetOwnerExists(int id)
        {
            try
            {
                _repo.PetOwner.FindOwnerWithId(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
