using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Infrustructure.NadinSoft.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.NadinSoft.Repository
{
    public class UserReadonlyRepository : IUserReadonlyRepository
    {
        private readonly NadinSoftDbcontext _context;

        public UserReadonlyRepository(NadinSoftDbcontext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<ApplicationUser?> GetByNationalCodeAsync(string nationalCode)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Nationalcode == nationalCode);
        }

    }
}
