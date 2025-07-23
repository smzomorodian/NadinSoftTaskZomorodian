using Application.NadinSoft.Command.ProductCommand;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.NadinSoft.CommandHandler.ProductCommandHandler
{
    public class DeletedProductCommandHandler : IRequestHandler<DeletedProductCommand, bool>
    {
        private readonly ICrudRepository<Product> _crudRepository;
        private readonly IGetProductRepository _getProductRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeletedProductCommandHandler(ICrudRepository<Product> crudRepository, IHttpContextAccessor httpContextAccessor, IGetProductRepository getProductRepository)
        {
            _crudRepository = crudRepository;
            _httpContextAccessor = httpContextAccessor;
            _getProductRepository = getProductRepository;

        }

        public async Task<bool> Handle(DeletedProductCommand request, CancellationToken cancellationToken)
        {
            //var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var product = await _getProductRepository.GetproductswhitProductId(request.ProductId);
            if (product == null)
                return false;

            //if (product.CreatedByUserId != currentUserId)
            //    return false;

            await _crudRepository.Deleted(product);
            await _crudRepository.SaveChange();
            return true;
        }
    }
}
