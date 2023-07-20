using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		ApplicationDbContext _dbContext;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
			return View(_dbContext.TaskList.ToList());
		}

		public IActionResult Create(TaskList taskList)
		{
			_dbContext.TaskList.Add(taskList);
			_dbContext.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			if (id == 0) return NotFound();

			var taskList = _dbContext.TaskList.FirstOrDefault(tl => tl.Id == id);

			if (taskList == null) return NotFound();

			_dbContext.TaskList.Remove(taskList);
			_dbContext.SaveChanges();


			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int id)
		{

			return View(_dbContext.Task.ToList());
		}

		public IActionResult CreateTask()
		{

			return View();
		}

		[HttpPost]
		public IActionResult CreateTask(Models.Task task, int id)
		{
			//TaskList taskList = _dbContext.TaskList.FirstOrDefault(tl => tl.Id == id - 1);
			//taskList.Task = (
			//	from TaskList in _dbContext.TaskList 
			//	select new Models.Task
			//	{
			//		Id = task.Id,
			//		Description = task.Description,
			//	}
			//	).ToList();
			//taskList.Task.Prepend(task);
			Models.Task task1 = new Models.Task() 
			{ 
				Description = task.Description,
			};
			_dbContext.Task.Add(task1);
			_dbContext.SaveChanges();

			return RedirectToAction(nameof(Details), new { id });

		}

		public IActionResult DeleteTask(int id)
		{
			if (id == 0) return NotFound();

			var task = _dbContext.Task.FirstOrDefault(tl => tl.Id == id);

			if (task == null) return NotFound();

			_dbContext.Task.Remove(task);
			_dbContext.SaveChanges();


			return RedirectToAction(nameof(Details));
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}