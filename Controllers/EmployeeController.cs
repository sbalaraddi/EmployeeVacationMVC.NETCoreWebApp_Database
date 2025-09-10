using EmployeeVacationDB.Data;
using EmployeeVacationDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeVacationDB.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        private static List<Employee> Employees = new List<Employee>();

        public EmployeeController(AppDbContext context)
        {
            _context = context;

            if (!_context.Employees.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    _context.Employees.Add(new HourlyEmployee { Name = $"Hourly-{i}" });
                    _context.Employees.Add(new SalariedEmployee { Name = $"Salaried-{i}" });
                    _context.Employees.Add(new Manager { Name = $"Manager-{i}" });
                }
                _context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
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

        [HttpPost]
        public IActionResult Work(int id, int days)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                try
                {
                    emp.Work(days);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TakeVacation(int id, float days)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                try
                {
                    emp.TakeVacation(days);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewCalender()
        {
            foreach(var emp in _context.Employees)
            {
                emp.TotalDaysWorked = 0;
                emp.VacationDaysUsed = 0;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
