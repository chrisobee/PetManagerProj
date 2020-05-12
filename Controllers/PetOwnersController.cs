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
using PetManager.ViewModels;

namespace PetManager.Controllers
{
    public class PetOwnersController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public PetOwnersController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: PetOwners
        public async Task<IActionResult> Index()
        {
            //Get userId and instantiate View Model
            TasksAndPetsVM tasksAndPets = new TasksAndPetsVM();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Find owner and set property on View Model
            var owner = await _repo.PetOwner.FindOwner(userId);
            tasksAndPets.PetOwner = owner;

            //Find all of the owner's pets and set prop on View Model
            var petIds = await _repo.PetOwnership.FindAllPets(owner.PetOwnerId);
            tasksAndPets.CurrentUsersPets = await FindOwnersPets(petIds);
            return View(tasksAndPets);
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

            var petOwner = await _context.PetOwners.FindAsync(id);
            if (petOwner == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", petOwner.IdentityUserId);
            return View(petOwner);
        }

        // POST: PetOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetOwnerId,Name,Lat,Lng,IdentityUserId")] PetOwner petOwner)
        {
            if (id != petOwner.PetOwnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petOwner);
                    await _context.SaveChangesAsync();
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", petOwner.IdentityUserId);
            return View(petOwner);
        }

        // GET: PetOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petOwner = await _context.PetOwners
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.PetOwnerId == id);
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
            var petOwner = await _context.PetOwners.FindAsync(id);
            _context.PetOwners.Remove(petOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetOwnerExists(int id)
        {
            return _context.PetOwners.Any(e => e.PetOwnerId == id);
        }
    }
}
