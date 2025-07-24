using Application.NadinSoft.Command.ProductCommand;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.CommandHandler.ProductCommandHandler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IGenericRepository<Product> _crudRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductReadonlyRepository _getProductRepository;

        public UpdateProductCommandHandler(IGenericRepository<Product> crudRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IProductReadonlyRepository getProductRepository)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _getProductRepository = getProductRepository;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var existingProduct = await _getProductRepository.GetById(request.ProductId);
            if (existingProduct == null)
                return "محصولی با این شناسه یافت نشد";

            if (existingProduct.CreatedByUserId != currentUserId)
                return "شما مجاز به ویرایش این محصول نیستید";

            
            existingProduct.Update(request.Name, request.ProduceDate, request.ManufacturePhone, request.ManufactureEmail, request.IsAvailable);

            await _crudRepository.Update(existingProduct);
            await _crudRepository.SaveChange();

            return "ویرایش با موفقیت انجام شد";
        }
    }
}
