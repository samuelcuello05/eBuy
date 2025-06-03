using eBuy.Clases;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Branches")]
    public class BranchesController : ApiController
    {
        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public string InsertBranch([FromBody] Branch branch)
        {
            clsBranch Branch = new clsBranch();
            Branch.branch = branch;
            return Branch.InsertBranch();
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public string UpdateBranch([FromBody] Branch branch)
        {
            clsBranch Branch = new clsBranch();
            Branch.branch = branch;
            return Branch.UpdateBranch();
        }

        [HttpGet]
        [Route("Search")]
        [Authorize]
        public Branch SearchBranch(string name)
        {
            clsBranch Branch = new clsBranch();
            return Branch.SearchBranch(name);
        }

        [HttpGet]
        [Route("SearchAll")]
        [Authorize]
        public List<Branch> SearchAllBranches()
        {
            clsBranch Branch = new clsBranch();
            return Branch.GetBranches();
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public string DeleteBranch(string name)
        {
            clsBranch Branch = new clsBranch();
            return Branch.DeleteBranch(name);
        }

        [HttpPut]
        [Route("UpdateOrAddItemToInventory")]
        [Authorize]
        public string UpdateOrAddItemToInventory(string branchName, int IdProduct, int quantity)
        {
            clsBranch Branch = new clsBranch();
            return Branch.UpdateOrAddItemToInventory(branchName, IdProduct, quantity);
        }

        [HttpPut]
        [Route("UpdateInventory")]
        [Authorize]
        public string UpdateInventory(string branchName, string productName, int quantity)
        {
            clsBranch Branch = new clsBranch();
            return Branch.UpdateInventory(branchName, productName, quantity);
        }

        [HttpPut]
        [Route("MoveEmployeeBetweenBranches")]
        [Authorize]

        public string MoveEmployeeBetweenBranches(int employeeId, string newBranchName)
        {
            clsBranch Branch = new clsBranch();
            return Branch.MoveEmployeeBetweenBranches(employeeId, newBranchName);
        }
    }
}