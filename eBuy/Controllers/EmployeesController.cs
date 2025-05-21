using eBuy.Clases;
using eBuy.Clases.DTOS;
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
    }
}