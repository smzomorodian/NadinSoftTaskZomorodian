using Application.NadinSoft.Command.ProductCommand;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.CommandHandler.ProductCommandHandler
{
    public class LoginUserCommandHandler : IRequestHandler<AddProductCommand, string>
    {
        private ICrudRepository<Product> _crudRepository;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(ICrudRepository<Product> crudRepository, IMapper mapper)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _crudRepository.Add(product);
            await _crudRepository.SaveChange();

            return product.Id.ToString();
        }
    }
}
