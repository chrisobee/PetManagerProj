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

namespace PetManager.Controllers
{
    public class PetsController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public PetsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

       
        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Pets/Create
        public IActionResult Create()
        {
            var pet = new Pet();
            return View(pet);
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pet pet)
        {
            if (ModelState.IsValid)
            {
                //Add pet to pet table
                _repo.Pet.CreatePet(pet);                
                await _repo.Save();

                //Add Pet to pet and owner jxn table
                await AddPetToJxnTable(pet);
                await _repo.Save();
                return RedirectToAction("index");
            }
            
            return View(pet);
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
            
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pet pet)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Pet.EditPet(pet);
                    await _repo.Save();
                    return RedirectToAction("index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }            
            return View(pet);
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
            return RedirectToAction("index");
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
