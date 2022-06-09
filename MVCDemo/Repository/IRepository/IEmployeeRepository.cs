using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Repository.IRepository
{
    interface IEmployeeRepository
    {
        ICollection<Employee> GetAllEmployees();
        Employee GetEmployee(int? id);        
        bool IsEmployeeExists(int? id);
        bool IsEmployeeExists(string email);
        bool InsertEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);        
        bool DeleteEmployee(int? id);
    }
}