using Microsoft.AspNetCore.Mvc;
using Vehicle_Config_DotNet_.Models;
namespace Vehicle_Config_DotNet_.Services
{
    public interface IUserRepository
    {
        public Task<ActionResult<CompanyInfo>> Add(CompanyInfo user);

        public Task<ActionResult<CompanyInfo>> GetUser(int id);
        //{
        //    //context.Employees.Add(employee);
        //    //await context.SaveChangesAsync();
        //    //return employee;
        //}

    }
}
