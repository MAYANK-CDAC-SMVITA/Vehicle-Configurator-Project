using Microsoft.AspNetCore.Mvc;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;

namespace Vehicle_Config_DotNet_.Services
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly ProjectContext context;
        public UserRepositoryImpl(ProjectContext _context)
        {
            context = _context;
        }
        public async Task<ActionResult<CompanyInfo>> Add(CompanyInfo user)
        {
            context.CompanyInfos.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<ActionResult<CompanyInfo>?> GetUser(int Id)
        {
            if (context.CompanyInfos == null)
            {
                return null;
            }
            var company = await context.CompanyInfos.FindAsync(Id);

            if (company == null)
            {
                return null;
            }

            return company;
        }

    }

}
