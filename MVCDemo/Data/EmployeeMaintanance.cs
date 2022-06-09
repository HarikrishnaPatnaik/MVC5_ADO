using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCDemo.Data
{
    public class EmployeeMaintenance
    {
        private string _connectionString;

        public EmployeeMaintenance()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["EmployeeDBContext"].ConnectionString.ToString();
        }

        /// <summary>
        /// Method retrieves all the employees
        /// </summary>
        /// <returns></returns>
        public ICollection<Employee> GetAllEmployees()
        {
            ICollection<Employee> listEmployees = new Collection<Employee>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {                    
                    SqlCommand cmd = new SqlCommand("Usp_GetAllEmployees", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(reader["Id"].ToString());
                        employee.FirstName = reader["FirstName"].ToString();
                        employee.LastName = reader["LastName"].ToString();
                        employee.Email = reader["Email"].ToString();
                        employee.Salary = Convert.ToDecimal(reader["Salary"].ToString());

                        listEmployees.Add(employee);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return listEmployees;
        }

        /// <summary>
        /// Method to retrieve employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployee(int? id)
        {
            Employee employee = new Employee();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {                    
                    SqlCommand cmd = new SqlCommand("Usp_GetEmployeeDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = SqlDbType.Int;
                    paramId.Value = id;
                    cmd.Parameters.Add(paramId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employee.Id = Convert.ToInt32(reader["Id"].ToString());
                        employee.FirstName = reader["FirstName"].ToString();
                        employee.LastName = reader["LastName"].ToString();
                        employee.Email = reader["Email"].ToString();
                        employee.Salary = Convert.ToDecimal(reader["Salary"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return employee;
        }

        /// <summary>
        /// Method to find an employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsEmployeeExists(int? id)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_IsEmployeeExists", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = SqlDbType.Int;
                    paramId.Value = id;
                    cmd.Parameters.Add(paramId);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.SqlDbType = SqlDbType.NVarChar;
                    paramEmail.Value = DBNull.Value;                    
                    cmd.Parameters.Add(paramEmail);

                    con.Open();
                    result = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Method to find an employee by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmployeeExists(string email)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_IsEmployeeExists", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = SqlDbType.Int;
                    paramId.Value = DBNull.Value;
                    cmd.Parameters.Add(paramId);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.SqlDbType = SqlDbType.NVarChar;
                    paramEmail.Value = email;                    
                    cmd.Parameters.Add(paramEmail);

                    con.Open();
                    result = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Method to insert Employee Records
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool InsertEmployee(Employee employee)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_InsertEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramFirstame = new SqlParameter();
                    paramFirstame.ParameterName = "@FirstName";
                    paramFirstame.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramFirstame.Value = employee.FirstName;
                    cmd.Parameters.Add(paramFirstame);

                    SqlParameter paramLastName = new SqlParameter();
                    paramLastName.ParameterName = "@LastName";
                    paramLastName.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramLastName.Value = employee.LastName;
                    cmd.Parameters.Add(paramLastName);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramEmail.Value = employee.Email;
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramSalary = new SqlParameter();
                    paramSalary.ParameterName = "@Salary";
                    paramSalary.SqlDbType = System.Data.SqlDbType.Decimal;
                    paramSalary.Value = employee.Salary;
                    cmd.Parameters.Add(paramSalary);

                    con.Open();
                    int resultInsert = cmd.ExecuteNonQuery();
                    result = resultInsert > 0 ? true : false;
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Method to update Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee employee)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_UpdateEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramId.Value = employee.Id;
                    cmd.Parameters.Add(paramId);

                    SqlParameter paramFirstame = new SqlParameter();
                    paramFirstame.ParameterName = "@FirstName";
                    paramFirstame.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramFirstame.Value = employee.FirstName;
                    cmd.Parameters.Add(paramFirstame);

                    SqlParameter paramLastName = new SqlParameter();
                    paramLastName.ParameterName = "@LastName";
                    paramLastName.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramLastName.Value = employee.LastName;
                    cmd.Parameters.Add(paramLastName);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramEmail.Value = employee.Email;
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramSalary = new SqlParameter();
                    paramSalary.ParameterName = "@Salary";
                    paramSalary.SqlDbType = System.Data.SqlDbType.Decimal;
                    paramSalary.Value = employee.Salary;
                    cmd.Parameters.Add(paramSalary);

                    con.Open();
                    int resultUpdate = cmd.ExecuteNonQuery();
                    result = resultUpdate > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Method to delete an employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int? id)
        {
            bool result = false;
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_DeleteEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = System.Data.SqlDbType.NVarChar;
                    paramId.Value = id;
                    cmd.Parameters.Add(paramId);

                    con.Open();
                    int resultDelete = cmd.ExecuteNonQuery();
                    result = resultDelete > 0 ? true : false;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        
    }
}