using Domain.NadinSoft.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Query
{
    public class ShowAllProductQuery : IRequest<List<Product>>
    {
    }
}
