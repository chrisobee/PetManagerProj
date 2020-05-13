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
    public class ToDoTasksController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public ToDoTasksController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        
        //GET: ToDoTasks/Details
        public async Task<IActionResult> Details(int taskId)
        {
            var task = await _repo.ToDoTask.FindTask(taskId);
            return View(task);
        }

        public async Task CheckOffTask(int? id)
        {
            var task = await _repo.ToDoTask.FindTask(id);
            task.TaskCompleted = true;

        }

        // GET: ToDoTasks/Create
        public async Task<IActionResult> Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = await _repo.PetOwner.FindOwner(userId);
            var petIds = await _repo.PetOwnership.FindAllPets(owner.PetOwnerId);


            TaskWithAllCurrentPets task = new TaskWithAllCurrentPets()
            {
                Task = new ToDoTask(),
                CurrentPets = await FindOwnersPets(petIds)
            };
            return View(task);
        }

        public async Task<List<Pet>> FindOwnersPets(List<int> petIds)
        {
            List<Pet> ownersPets = new List<Pet>();
            foreach (int id in petIds)
            {
                var results = await _repo.Pet.FindByCondition(p => p.PetId == id);
                ownersPets.Add(results.FirstOrDefault());
            }
            return ownersPets;
        }
        // POST: ToDoTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskWithAllCurrentPets toDoTask)
        {
            if (ModelState.IsValid)
            {
                _repo.ToDoTask.CreateTask(toDoTask.Task);
                await _repo.Save();
                return RedirectToAction("Index", "PetOwners");
            }
            return View(toDoTask);
        }

        // GET: ToDoTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _repo.ToDoTask.FindTask(id);
            if (toDoTask == null)
            {
                return NotFound();
            }
            return View(toDoTask);
        }

        // POST: ToDoTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoTask toDoTask)
        {
            if (id != toDoTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.ToDoTask.Update(toDoTask);
                    await _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoTaskExists(toDoTask.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PetOwners");
            }
            return View(toDoTask);
        }

        // GET: ToDoTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _repo.ToDoTask.FindTask(id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            return View(toDoTask);
        }

        // POST: ToDoTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoTask = await _repo.ToDoTask.FindTask(id);
            _repo.ToDoTask.DeleteTask(toDoTask);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoTaskExists(int id)
        {
            try
            {
                _repo.ToDoTask.FindTask(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
