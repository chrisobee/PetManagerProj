using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetManager.Data;
using PetManager.Models;

namespace PetManager.Controllers
{
    public class ToDoTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDoTask.Include(t => t.Pet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDoTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _context.ToDoTask
                .Include(t => t.Pet)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            return View(toDoTask);
        }

        // GET: ToDoTasks/Create
        public IActionResult Create()
        {
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "PetId");
            return View();
        }

        // POST: ToDoTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,TaskName,TakeCompleted,TaskInterval,SpecialInstructions,PetId")] ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "PetId", toDoTask.PetId);
            return View(toDoTask);
        }

        // GET: ToDoTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _context.ToDoTask.FindAsync(id);
            if (toDoTask == null)
            {
                return NotFound();
            }
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "PetId", toDoTask.PetId);
            return View(toDoTask);
        }

        // POST: ToDoTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskName,TakeCompleted,TaskInterval,SpecialInstructions,PetId")] ToDoTask toDoTask)
        {
            if (id != toDoTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoTask);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetId"] = new SelectList(_context.Pet, "PetId", "PetId", toDoTask.PetId);
            return View(toDoTask);
        }

        // GET: ToDoTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _context.ToDoTask
                .Include(t => t.Pet)
                .FirstOrDefaultAsync(m => m.TaskId == id);
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
            var toDoTask = await _context.ToDoTask.FindAsync(id);
            _context.ToDoTask.Remove(toDoTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoTaskExists(int id)
        {
            return _context.ToDoTask.Any(e => e.TaskId == id);
        }
    }
}
