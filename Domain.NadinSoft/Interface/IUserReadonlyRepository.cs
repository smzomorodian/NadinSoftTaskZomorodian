using Domain.NadinSoft.Model;

namespace Domain.NadinSoft.Interface
{
    public interface IUserReadonlyRepository
    {
        Task<ApplicationUser?> GetByNationalCodeAsync(string nationalCode);
        Task<ApplicationUser?> GetByIdAsync(string userId);
    }
}
