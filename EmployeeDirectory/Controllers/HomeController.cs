using EmployeeDirectory.Data.Interface;
using EmployeeDirectory.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeDirectory.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SelectList _sectionList;

        public HomeController(IEmployeeRepository _employeeRepository, ISectionRepository _sectionRepository, IWebHostEnvironment _webHostEnvironment)
        {
            this._employeeRepository = _employeeRepository;
            this._sectionRepository = _sectionRepository;
            this._webHostEnvironment = _webHostEnvironment;
            _sectionList = new(_sectionRepository.GetAll(), "SectionId", "Descript");
        }

        public IActionResult Index(string searchString, int pg = 1)
        {
            IEnumerable<Employee> model = _employeeRepository.GetAllEmployees();
            //Фильтр
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.ToLower().Contains(searchString) ||
                x.Surname.ToLower().Contains(searchString) ||
                x.Midlname.ToLower().Contains(searchString) ||
                x.PhoneNumber.ToString().Contains(searchString));
            }
            //Код пагинации
            const int pageSize = 5;
            if (pg < 1) pg = 1;
            int recsCount = model.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            model = model.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(model);
        }

        // GET: ViewController/Details/5
        public ActionResult Details(int id) => View(_employeeRepository.GetEmployee(id));

        // GET: ViewController/Create
        public ActionResult Create()
        {
            ViewData["Sections"] = _sectionList;
            return View();
        }

        // POST: ViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee model)
        {
            try
            {
                model.UrlPhoto = UploadedFile(model);
                model.Section = _sectionRepository.GetById(int.Parse(Request.Form["Sections"]));
                _employeeRepository.CreateEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //Честно скажу что не знаю по какой причине этот эндпоинт вызывается дважды
        // GET: ViewController/Edit/5
        public ActionResult Edit(int id)
        {
            Employee model = _employeeRepository.GetEmployee(id);
            SelectList list = _sectionList;
            list.First(x => x.Text.Contains(model.Section.Descript)).Selected = true;

            ViewData["Sections"] = list;
            return View(model);
        }

        // POST: ViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee model)
        {
            try
            {
                if (model.PhotoForm != null) model.UrlPhoto = UploadedFile(model);
                model.Section = _sectionRepository.GetById(int.Parse(Request.Form["Sections"]));
                _employeeRepository.UpdateEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewController/Delete/5
        public ActionResult Delete(int id) => View(_employeeRepository.GetEmployee(id));

        // POST: ViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee model)
        {
            try
            {
                _employeeRepository.DeleteEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string UploadedFile(Employee employee)
        {
            string uniqueFileName = null;
            if (employee.PhotoForm != null)

            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "sourse/images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.PhotoForm.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    employee.PhotoForm.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

    }
}
