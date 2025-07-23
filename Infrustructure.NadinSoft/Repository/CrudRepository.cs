using Domain.NadinSoft.Interface;
using Infrustructure.NadinSoft.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrustructure.NadinSoft.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class
    {
        private readonly APPDbcontext _aPPDbcontext;

        public CrudRepository(APPDbcontext aPPDbcontext)
        {
            _aPPDbcontext = aPPDbcontext;
        }
        public async Task<string> Add(T entity)  
        {
            await _aPPDbcontext.Set<T>().AddAsync(entity);
            return entity.ToString();

        }

        public  async Task<bool> Deleted(T entity)
        {
             _aPPDbcontext.Set<T>().Remove(entity);
            return await Task.FromResult(true);
        }

        public async Task<List<T>> GetAll()
        {
            return await _aPPDbcontext.Set<T>().ToListAsync();
        }

        public async Task SaveChange()
        {
            await _aPPDbcontext.SaveChangesAsync();
        }

        public async Task<bool> Update(T entity)
        {
            _aPPDbcontext.Set<T>().Update(entity);
            return await Task.FromResult(true);
        }
    }
}
