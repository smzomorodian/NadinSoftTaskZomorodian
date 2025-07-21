using Domain.NadinSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NadinSoft.Interface
{
    public interface ICrudRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<string> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Deleted(Product product);
        Task<bool> SaveChange();
    }
}
