using Domain.NadinSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NadinSoft.Interface
{
    public interface IGetProductRepository
    {
        Task<List<Product>> GetProducts(string UserId);
        Task<Product> GetproductswhitProductId(Guid id);
    }
}
