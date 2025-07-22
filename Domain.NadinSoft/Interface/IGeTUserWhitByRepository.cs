using Domain.NadinSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NadinSoft.Interface
{
    public interface IGeTUserWhitByRepository
    {
        Task<ApplicationUser> GetByNationalCode(string nationalCode);
    }
}
