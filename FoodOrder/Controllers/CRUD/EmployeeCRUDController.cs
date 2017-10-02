using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers.CRUD
{
    public class EmployeeCRUDController : Controller
    {
        private IEmployeeRepository employeeRepository;

        public EmployeeCRUDController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        // GET: EmployeeCRUD
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var model = employeeRepository.GetAll()
                .Select(t => new EmployeeCRUDViewModel
                {
                    City = t.City,
                    Email = t.Email,
                    EmployeeID = t.EmployeeID,
                    FirstName = t.FirstName,
                    HireDate = t.HireDate.ToLongDateString(),
                    IsDeleted = t.IsDeleted,
                    LastName = t.LastName,
                    Phone = t.Phone,
                    PostCode = t.PostCode,
                    Role = t.Role.ToString(),
                    Salary = t.Salary,
                    Street = t.Street
                })
                .ToList();

            return PartialView("GetAll", model);
        }

        public ActionResult GetById(int? employeeId)
        {
            if(employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = employeeRepository.GetById(employeeId);

            EmployeeCRUDViewModel model = new EmployeeCRUDViewModel
            {
                City = employee.City,
                Email = employee.Email,
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName,
                HireDate = employee.HireDate.ToLongDateString(),
                IsDeleted = employee.IsDeleted,
                LastName = employee.LastName,
                Phone = employee.Phone,
                PostCode = employee.PostCode,
                Role = employee.Role.ToString(),
                Salary = employee.Salary,
                Street = employee.Street
            };

            return Json(model, JsonRequestBehavior.AllowGet);
                
        }

        public ActionResult Delete(int? employeeId)
        {
            if(employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            employeeRepository.Remove(employeeId);

            return RedirectToAction("GetAll");
        }
    }
}