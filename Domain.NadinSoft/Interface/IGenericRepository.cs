using Domain.NadinSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NadinSoft.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<string> Add(T enttity);
        Task<bool> Update(T enttity);
        Task<bool> Deleted(T enttity);
        Task SaveChange();
    }
}
