using eBuy.Clases;
using eBuy.Clases.DTOS;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        [HttpPost]
        [Route("Register")]
        public string CreateEmployee([FromBody] clsUserEmployeeDTO dto, string branchName)
        {
            clsEmployee employee = new clsEmployee();
            return employee.CreateEmployee(dto.User, dto.Employee, branchName);
        }

        [HttpGet]
        [Route("List")]
        public List<Employee> GetAllEmployees()
        {
            clsEmployee employee = new clsEmployee();
            return employee.GetAllEmployees();
        }

        [HttpGet]
        [Route("GetById")]
        public Employee GetEmployeeById(int IdEmployee)
        {
            clsEmployee employee = new clsEmployee();
            return employee.GetEmployeeById(IdEmployee);
        }
    }
}