using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public ActionResult Index()
        {
            //return View(_employeeRepository.GetAllEmployee());

            return RedirectToAction("List");
        }

        public ViewResult Details(int id)
        {
            //throw new Exception("erro no método details");

            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogDebug("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");


            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel
            {
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
        }

        public ViewResult List()
        {
            return View(_employeeRepository.GetAllEmployee());
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            //var x1 = employee;

            _logger.LogTrace("Trace");
            _logger.LogDebug("Debug");
            _logger.LogDebug("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");

            Employee employee = _employeeRepository.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee editEmployee = _employeeRepository.GetEmployee(model.Id);

                editEmployee.Name = model.Name;
                editEmployee.Email = model.Email;
                editEmployee.Department = model.Department;

                if (model.Photo != null) { 
                 
                    if (model.ExistingPhotoPath != null)
                        System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath));
                    
                    editEmployee.PhotoPath = ProcessUploadFile(model);
                }
                _employeeRepository.Update(editEmployee);
                return RedirectToAction("Details", new { editEmployee.Id });
            }

            return View(model);
        }

        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = string.Concat(Guid.NewGuid().ToString(), "_", model.Photo.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Photo.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { newEmployee.Id });
            }

            return View();
        }
    }
}
