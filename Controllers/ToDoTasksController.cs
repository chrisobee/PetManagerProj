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
        public async Task<IActionResult> Details(int? taskId)
        {
            var task = await _repo.ToDoTask.FindTask(taskId);
            var pet = await _repo.Pet.GetPet(task.PetId);
            ViewBag.Pet = pet;
            return View(task);
        }

        public async Task<IActionResult> ContactTaskDetails(int taskId, int contactId)
        {
            var task = await _repo.ToDoTask.FindTask(taskId);
            ViewBag.contactId = contactId;
            ViewBag.pet = task.Pet;
            return View(task);
        }

        public async Task<IActionResult> CheckOffTask(int id, int currentUserId)
        {
            var task = await _repo.ToDoTask.FindTask(id);
            switch (task.Frequency.Interval)
            {
                case "Daily":
                    task.ResetDay = DateTime.Today.Date;
                    break;
                case "Weekly":
                    TimeSpan weekly = new TimeSpan(5, 0, 0, 0);
                    task.ResetDay = DateTime.Today.Date + weekly;
                    break;
                case "Monthly":
                    TimeSpan monthly = new TimeSpan(19, 0, 0, 0);
                    task.ResetDay = DateTime.Today.Date + monthly;
                    break;
                default:
                    task.ResetDay = DateTime.Today.Date;
                    break;
            }
            _repo.ToDoTask.EditTask(task);
            await _repo.Save();

            //Check Redirect Location
            if (await CheckRedirectLocation(currentUserId))
            {
                return RedirectToAction("Index", "PetOwners");
            }
            else
            {
                return RedirectToAction("IndexForContact", "PetOwners", new { contactId = currentUserId });
            }
        }

        public async Task<bool> CheckRedirectLocation(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserId = await _repo.PetOwner.FindOwnerId(userId);
            if(currentUserId == id)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                CurrentPets = await FindOwnersPets(petIds),
                AllFrequencies = await _repo.Frequency.GetFrequencies()
            };
            return View(task);
        }

        public async Task<IActionResult> CreateBasedOnRecommendation(string taskName, int petId, string interval)
        {
            TimeSpan defaultTimeSpan = new TimeSpan(1, 0, 0, 0);
            ToDoTask taskToAdd = new ToDoTask()
            {
                TaskName = taskName,
                PetId = petId,
                ResetDay = DateTime.Today.Date - defaultTimeSpan,
                FrequencyId = await _repo.Frequency.GetFrequencyByIntervalName(interval)
            };

            _repo.ToDoTask.CreateTask(taskToAdd);
            await _repo.Save();

            return RedirectToAction("Index", "PetOwners");
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
                TimeSpan defaultTime = new TimeSpan(1, 0, 0, 0);
                toDoTask.Task.ResetDay = DateTime.Today.Date - defaultTime;
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
            //Find all pets so user can choose the pet that this task targets
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ownerId = await _repo.PetOwner.FindOwnerId(userId);

            //Get all pets and places it in viewbag
            List<int> petIds = await _repo.PetOwnership.FindAllPets(ownerId);
            List<Pet> pets = await GetPetsFromIds(petIds);
            ViewBag.Pets = pets;

            //Get all frequencies and places them in viewbag
            List<Frequency> frequencies = await _repo.Frequency.GetFrequencies();
            ViewBag.Frequencies = frequencies;

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

        public async Task<List<Pet>> GetPetsFromIds(List<int> petIds)
        {
            List<Pet> pets = new List<Pet>();
            foreach(int id in petIds)
            {
                var pet = await _repo.Pet.GetPet(id);
                pets.Add(pet);
            }
            return pets;
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
            return RedirectToAction("index", "PetOwners");
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
