﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleToDo.Model.Entities;
using SimpleToDo.Model.Extensions;
using SimpleToDo.Model.ViewModels;
using SimpleToDo.Service.Contracts;
using Task = SimpleToDo.Model.Entities.Task;

namespace SimpleToDo.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IToDoListService _toDoListService;

        public TaskController(ITaskService taskService, IToDoListService toDoListService)
        {
            _taskService = taskService;
            _toDoListService = toDoListService;
        }

        // GET: Task
        public async Task<IActionResult> Index(int listId)
        {
            List toDoList = await _toDoListService.GetToDoListById(listId);

            if (toDoList == null)
                return NotFound();

            var taskIndexViewModel = new TaskIndexViewModel
            {
                ListId = listId,
                ListName = toDoList.Name,
                ToDoTasks = toDoList.Tasks.Where(t => t.Done == false).OrderByDescending(x => x.TaskId),
                CompletedTasks = toDoList.Tasks.Where(t => t.Done).OrderByDescending(x => x.TaskId)
            };

            return base.View(taskIndexViewModel);
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _taskService.GetTaskById(id.Value);

            if (task == null)
                return NotFound();

            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create(int listId)
        {
            return View(new TaskCreateEditViewModel
            {
                ListId = listId
            });
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,ListId,Name,Description,Done")] Task task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTask(task);

                this.AddAlertSuccess($"{task.Name} created successfully.");

                return RedirectToAction(nameof(Index), new { listId = task.ListId });
            }

            return View(new TaskCreateEditViewModel
            {
                ListId = task.ListId,
                Name = task.Name,
                Description = task.Description
            });
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _taskService.GetTaskById(id.Value);
            //_context.Task.SingleOrDefaultAsync(m => m.TaskId == id);

            if (task == null)
                return NotFound();

            return View(new TaskCreateEditViewModel
            {
                TaskId = task.TaskId,
                ListId = task.ListId,
                Name = task.Name,
                Description = task.Description,
                Done = task.Done
            });
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,ListId,Name,Description,Done")] Task task)
        {
            if (id != task.TaskId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _taskService.UpdateTask(task);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var taskExists = await _taskService.TaskExists(task.TaskId);
                    if (!taskExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                this.AddAlertSuccess($"{task.Name} updated successfully.");
                return RedirectToAction(nameof(Index), new { listId = task.ListId });
            }

            return View(new TaskCreateEditViewModel
            {
                TaskId = task.TaskId,
                ListId = task.ListId,
                Name = task.Name,
                Description = task.Description,
                Done = task.Done
            });
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var task = await _taskService.GetTaskById(id.Value);
            
            if (task == null)            
                return NotFound();            

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _taskService.RemoveTask(id);
           
            this.AddAlertSuccess($"{task.Name} removed successfully.");

            return RedirectToAction(nameof(Index), new { listId = task.ListId });
        }


        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _taskService.CompleteTask(id);

            this.AddAlertSuccess($"{task.Name} completed successfully.");

            return RedirectToAction(nameof(Index), new { listId = task.ListId });
        }

        public async Task<IActionResult> UndoTask(int id)
        {
            var task = await _taskService.UndoTask(id);

            this.AddAlertSuccess($"{task.Name} undone successfully.");

            return RedirectToAction(nameof(Index), new { listId = task.ListId });
        }
    }
}