using MVCDemo.Models;
using MVCDemo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Holds the Employee Collection
        /// </summary>
        private ICollection<Employee> _listEmployees;

        /// <summary>
        /// Holds teh EmployeeRepository Object
        /// </summary>
        private EmployeeRepository _employeeRepository;

        /// <summary>
        /// Contructor to initialize necessary objects
        /// </summary>
        public EmployeeController()
        {
            _listEmployees = new Collection<Employee>();
            _employeeRepository = new EmployeeRepository();
        }

        /// <summary>
        /// Default Action method of controller retreives Employee Collection
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {            
            try
            {
                _listEmployees = _employeeRepository.GetAllEmployees();
                if(_listEmployees != null && _listEmployees.Count > 0)
                    return View(_listEmployees);
            }
            catch(Exception ex)
            {
                return Content("Error occured while loading the Employees.");
            }
            return View();
        }

        /// <summary>
        /// Get Request to load the create View.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post Request to Create an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isExists = _employeeRepository.IsEmployeeExists(employee.Email);
                    if (!isExists)
                    {
                        bool result = _employeeRepository.InsertEmployee(employee);
                        if (result)
                            //return RedirectToAction("Index");
                            return Json(new { redirectToUrl = Url.Action("Index", "Employee") });
                    }
                    else
                        return Content("Employee already exists.");
                }
                catch(Exception ex)
                {
                    return Content("Error occurred while creating an employee.");
                }
                
            }            
            return View();
        }

        /// <summary>
        /// Get request to load update view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(int? id)
        {
            try
            {                
                bool isExists = _employeeRepository.IsEmployeeExists(id);
                if (isExists)
                {
                    Employee employee = _employeeRepository.GetEmployee(id);
                    return View(employee);
                }
                else
                {
                    return Content("Employee details not found try again.");
                }
            }
            catch(Exception ex)
            {
                return Content("Error while Loading employee details.");
            }
        }

        /// <summary>
        /// Post request to update Employee Records
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isExists = _employeeRepository.IsEmployeeExists(employee.Id);
                    if (isExists)
                    {
                        bool result = _employeeRepository.UpdateEmployee(employee);
                        if (result)
                            return RedirectToAction("Index");
                        else
                            return Content("Failed to update employee record.");
                    }
                }
                catch(Exception ex)
                {
                    return Content("Error occurred while updating the employee.");
                }
            }            
            return View();
        }

        /// <summary>
        /// Get Request to load delete view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                bool isExists = _employeeRepository.IsEmployeeExists(id);
                if (isExists)
                {
                    Employee employee = _employeeRepository.GetEmployee(id);
                    ViewBag.EmployeeId = employee.Id;
                    return View(employee);
                }
                else
                    return Content("Employee not found.");
            }
            catch(Exception ex)
            {
                return Content("Error occurred while loading the employee.");
            }
        }

        /// <summary>
        /// Post request to delete employee record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int? id)
        {
            try
            {
                bool isExists = _employeeRepository.IsEmployeeExists(id);
                if (isExists)
                {
                    bool result = _employeeRepository.DeleteEmployee(id);
                    if (result)
                        return RedirectToAction("Index");                    
                }
                else
                    return Content("Employee not found.");
            }
            catch (Exception ex)
            {
                return Content("Error occurred while loading the employee.");
            }
            return View();
        }        
    }
}