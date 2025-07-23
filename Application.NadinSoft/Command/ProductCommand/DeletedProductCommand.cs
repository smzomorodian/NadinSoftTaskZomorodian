using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.Command.ProductCommand
{
    public class DeletedProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
    }
}
