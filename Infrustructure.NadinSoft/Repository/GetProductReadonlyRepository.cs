using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Infrustructure.NadinSoft.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.NadinSoft.Repository
{
    public class GetProductReadonlyRepository : IProductReadonlyRepository
    {
        private readonly NadinSoftDbcontext _context;
        public GetProductReadonlyRepository(NadinSoftDbcontext context)
        {
            _context = context;
        }
        public async Task<List<Product>?> GetAllProductByUserId(string userId) => 
            await _context.products
            .AsNoTracking()
            .Where(u => u.CreatedByUserId == userId).ToListAsync();


        public async Task<Product?> GetById(Guid id) => await _context.products.FindAsync(id);
    }
}
