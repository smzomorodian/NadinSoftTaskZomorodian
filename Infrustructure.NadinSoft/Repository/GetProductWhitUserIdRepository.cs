using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Infrustructure.NadinSoft.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrustructure.NadinSoft.Repository
{
    public class GetProductWhitUserIdRepository : IGetProductWhitUserIdRepository
    {
        private readonly APPDbcontext _aPPDbcontext;
        public GetProductWhitUserIdRepository(APPDbcontext aPPDbcontext)
        {
            _aPPDbcontext = aPPDbcontext;
        }
        public async Task<List<Product>> GetProductsWhitUserId(string UserId)
        {
            var user = await _aPPDbcontext.products.Where(u => u.CreatedByUserId == UserId).ToListAsync();
            return user;
        }
    }
}
