using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using Infrustructure.NadinSoft.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.NadinSoft.Repository
{
    public class GeTUserWhitByRepository : IGeTUserWhitByRepository
    {
        private readonly APPDbcontext _aPPDbcontext;
        public GeTUserWhitByRepository(APPDbcontext aPPDbcontext)
        {
            _aPPDbcontext = aPPDbcontext;
        }

        public async Task<ApplicationUser> GetByNationalCode(string nationalCode)
        {
            return await _aPPDbcontext.Users.FirstOrDefaultAsync(u => u.Nationalcode == nationalCode);
        }
    }
}
