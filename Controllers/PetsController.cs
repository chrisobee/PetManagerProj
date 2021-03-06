﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using PetManager.Contracts;
using PetManager.Data;
using PetManager.Models;
using PetManager.ViewModels;

namespace PetManager.Controllers
{
    public class PetsController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PetsController(IRepositoryWrapper repo, IWebHostEnvironment hostingEnvironment)
        {
            _repo = repo;
            _hostingEnvironment = hostingEnvironment;
        }

       
        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            PetsAndAnimalTypeVM petsAndAnimalTypeVM = new PetsAndAnimalTypeVM();
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _repo.Pet.GetPet(id);

            if (pet == null)
            {
                return NotFound();
            }
            petsAndAnimalTypeVM.Pet = pet;
            
            AnimalType animalType = await _repo.AnimalType.GetAnimalTypeById(pet.AnimalTypeId);
            petsAndAnimalTypeVM.AnimalTypes = new List<AnimalType>();
            petsAndAnimalTypeVM.AnimalTypes.Add(animalType);
            petsAndAnimalTypeVM.PetsTasks = new List<ToDoTask>();
            var PetTasks = await _repo.ToDoTask.GetTasksByPet(pet.PetId);
            foreach (ToDoTask task in PetTasks) 
            {
                petsAndAnimalTypeVM.PetsTasks.Add(task);
            }             
            return View(petsAndAnimalTypeVM);
        }

        public async Task<IActionResult> ContactPetDetails(int? id, int contactId)
        {
            ViewBag.contactId = contactId;
            PetsAndAnimalTypeVM petsAndAnimalTypeVM = new PetsAndAnimalTypeVM();

            if (id == null)
            {
                return NotFound();
            }

            var pet = await _repo.Pet.GetPet(id);

            if (pet == null)
            {
                return NotFound();
            }
            petsAndAnimalTypeVM.Pet = pet;
            var results = await _repo.ToDoTask.GetTasksByPet(pet.PetId);
            petsAndAnimalTypeVM.PetsTasks = results.ToList();
            return View(petsAndAnimalTypeVM);
        }

        // GET: Pets/Create
        public async Task<IActionResult> Create()
        {
            PetsAndAnimalTypeVM pet = new PetsAndAnimalTypeVM()
            {
                AnimalTypes = await GetAnimalTypes(),
                Pet = new Pet()
            };
            
            return View(pet);
        }

        public async Task<List<AnimalType>> GetAnimalTypes()
        {
            var results = await _repo.AnimalType.GetAnimalTypes();
            var types = results.ToList();
            return types;
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetsAndAnimalTypeVM petVM)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(petVM.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + petVM.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    petVM.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                //Add pet to pet table
                petVM.Pet.PhotoPath = uniqueFileName;
                _repo.Pet.CreatePet(petVM.Pet);                
                await _repo.Save();
                await AddPetToJxnTable(petVM.Pet);
                await _repo.Save();
                return RedirectToAction("Index", "PetOwners");
            }
            
            return View(petVM.Pet);
        }

        public async Task AddPetToJxnTable(Pet pet)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var PetOwnerId = await _repo.PetOwner.FindOwnerId(userId);
            _repo.PetOwnership.Create(PetOwnerId, pet.PetId);
        }
        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var pet = await _repo.Pet.GetPet(id);
            if (pet == null)
            {
                return NotFound();
            }

            PetsAndAnimalTypeVM petVM = new PetsAndAnimalTypeVM()
            {
                Pet = pet,
                AnimalTypes = await GetAnimalTypes(),
                 
            };
            return View(petVM);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PetsAndAnimalTypeVM petVM)
        {
            if (id != petVM.Pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = null;
                    if (petVM.Photo != null)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + petVM.Photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        petVM.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        petVM.Pet.PhotoPath = uniqueFileName;
                    }
                    
                    _repo.Pet.EditPet(petVM.Pet);
                    await _repo.Save();                   
                    return RedirectToAction("Details", new { id = petVM.Pet.PetId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(petVM.Pet.PetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }            
            return View(petVM);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _repo.Pet.GetPet(id);
            if (pet == null)
            {
                return NotFound();
            }
            
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _repo.Pet.GetPet(id);
            _repo.Pet.DeletePet(pet);
            await _repo.Save();
            return RedirectToAction("index", "PetOwners");
        }

        private bool PetExists(int id)
        {
            try
            {
                _repo.Pet.GetPet(id);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
