using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeAPI.Models
{
    public class EmpModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}