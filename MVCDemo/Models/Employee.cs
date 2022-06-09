using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "FirstName is a required field.")]
        [MaxLength(30,ErrorMessage ="First Name length should not greater than 30 characters.")]
        [MinLength(3,ErrorMessage ="First name should be more than 3 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        [MaxLength(30, ErrorMessage = "First Name length should not greater than 30 characters.")]
        [MinLength(3, ErrorMessage = "First name should be more than 3 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(100,ErrorMessage ="Email should not greater than 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage ="Invalid Email address.")]
        public string Email { get; set; }        

        [Required]        
        public decimal Salary { get; set; }
    }
}