using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Services;

namespace VehicleDotNet.Controllers
{
    public class RegistrationController : Controller
    {
       

        private IUserRepository user;
        public RegistrationController(IUserRepository _user)
        {
            user = _user;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleAsync(int id)
        {
            if (id> 10) 
                throw new ApplicationException("Invalid ID");
            var vehicle = await user.GetUser(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPost("api/new_Company")]
        public async Task<ActionResult<CompanyInfo>> PostUser([FromBody]CompanyInfo company)
        {
            await user.Add(company);
            return CreatedAtAction("PostUser", new { CompanyId = company.CompanyId }, company);
            
        }

    }
}
