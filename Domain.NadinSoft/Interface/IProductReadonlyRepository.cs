using Domain.NadinSoft.Model;

namespace Domain.NadinSoft.Interface
{
    public interface IProductReadonlyRepository
    {
        Task<List<Product>?> GetAllProductByUserId(string UserId);
        Task<Product?> GetById(Guid id);
    }
}
