using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeAPI.Models;
using System.Text;
using System.Web;


namespace EmployeeAPI.Controllers
{
    public class HomeController : ApiController
    {
        string filepath = HttpContext.Current.Server.MapPath("/Files/data.json");
            //@"C:\Users\91775\data.json";
    
        [HttpPost]
        public bool AddEmpDetails(EmpModel emp)
        {           
            var json = File.ReadAllText(filepath);
            var list = JsonConvert.DeserializeObject<List<EmpModel>>(json);
            EmpModel em = new EmpModel();
            em.Id =  emp.Id;
            em.EmployeeName = emp.EmployeeName;
            em.PhoneNumber = emp.PhoneNumber;
            em.EmailAddress= emp.EmailAddress;
            list.Add(em);
            string jsontooutput = JsonConvert.SerializeObject(list);
            File.WriteAllText(filepath, jsontooutput);
            return true;

        }
        
        [HttpGet]
        public object GetEmpDetails()
        {
            
            string allText = System.IO.File.ReadAllText(filepath);
            object jsonObject = JsonConvert.DeserializeObject(allText);
            return jsonObject;
        }
        
        [HttpDelete]
        public string DeleteEmpDetails(int Id)
        {            
            JArray employeeArrary = JArray.Parse(File.ReadAllText(filepath));
            try
            {
                if (Id > 0)
                {
                   
                    var idToDeleted = employeeArrary.FirstOrDefault(obj => obj["Id"].Value<int>() == Id);
                                           
                            employeeArrary.Remove(idToDeleted);
                        
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(employeeArrary);
                    File.WriteAllText(filepath, output);
                    
                }
                return "Employee Deleted!";
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut]
        public string UpdateEmpDetails(EmpModel emp)
        {
            
            JArray employeeArrary = JArray.Parse(File.ReadAllText(filepath));
            try
            {
                
                if (emp.Id > 0)
                {              
                
                foreach (var employee in employeeArrary.Where(obj => obj["Id"].Value<int>() == emp.Id))
                    {
                       employee["EmployeeName"] = !string.IsNullOrEmpty(emp.EmployeeName) ? emp.EmployeeName : "";
                        employee["PhoneNumber"] = !string.IsNullOrEmpty(emp.PhoneNumber) ? emp.PhoneNumber : "";
                        employee["EmailAddress"] = !string.IsNullOrEmpty(emp.EmailAddress) ? emp.EmailAddress : "";
                        
                    }                    
              
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(employeeArrary);
                    File.WriteAllText(filepath, output);
                    return "Employee details updated!";
                }
                else
                {
                    return "Invalid Employee ID, Try Again!";

                }
            }
            catch (Exception)
            {

                throw;
            }            

        }

    }
}
