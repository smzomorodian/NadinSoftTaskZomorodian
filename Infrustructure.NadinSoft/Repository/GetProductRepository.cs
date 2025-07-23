using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Infrustructure.NadinSoft.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.NadinSoft.Repository
{
    public class GetProductRepository : IGetProductRepository
    {
        private readonly APPDbcontext _aPPDbcontext;
        public GetProductRepository(APPDbcontext aPPDbcontext)
        {
            _aPPDbcontext = aPPDbcontext;
        }
        public async Task<List<Product>> GetProducts(string UserId)
        {
            var user = await _aPPDbcontext.products.Where(u => u.CreatedByUserId == UserId).ToListAsync();
            return user;
        }

        public async Task<Product> GetproductswhitProductId(Guid id)
        {
            return await _aPPDbcontext.products.FindAsync(id);
        }
    }
}
