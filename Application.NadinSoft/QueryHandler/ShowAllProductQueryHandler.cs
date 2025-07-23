using Application.NadinSoft.Query;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.QueryHandler
{
    public class ShowAllProductQueryHandler : IRequestHandler<ShowAllProductQuery, List<Product>>
    {
        private readonly ICrudRepository<Product> _crudRepository;
        public ShowAllProductQueryHandler(ICrudRepository<Product> crudRepository)
        {
            _crudRepository = crudRepository;
        }

        public async Task<List<Product>> Handle(ShowAllProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _crudRepository.GetAll();
            return product.ToList();
        }
    }
}
