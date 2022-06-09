using MVCDemo.Data;
using MVCDemo.Models;
using MVCDemo.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeMaintenance _employeeMaintenance;

        public EmployeeRepository()
        {
            _employeeMaintenance = new EmployeeMaintenance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int? id)
        {            
            return _employeeMaintenance.DeleteEmployee(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<Employee> GetAllEmployees()
        {            
            return _employeeMaintenance.GetAllEmployees();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployee(int? id)
        {            
            return _employeeMaintenance.GetEmployee(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool InsertEmployee(Employee employee)
        {            
            return _employeeMaintenance.InsertEmployee(employee);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsEmployeeExists(int? id)
        {           
            return _employeeMaintenance.IsEmployeeExists(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmployeeExists(string email)
        {            
            return _employeeMaintenance.IsEmployeeExists(email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee updateEmployee)
        {            
            return  _employeeMaintenance.UpdateEmployee(updateEmployee);            
        }
    }
}