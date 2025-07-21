using Domain.NadinSoft.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.NadinSoft.Context
{
    public class APPDbcontext : DbContext
    {
        protected APPDbcontext()
        {
        }
        public APPDbcontext(DbContextOptions<APPDbcontext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
    }
}
