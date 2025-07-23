using Application.NadinSoft.Query;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;

namespace Application.NadinSoft.QueryHandler
{
    public class ShowProductWhitUserIdQueryHandler : IRequestHandler<ShowProductWhitUserIdQuery, List<Product>>
    {
        private readonly IGetProductRepository _getProductWhitUserId;
        private IMapper _mapper;
        public ShowProductWhitUserIdQueryHandler(IGetProductRepository getProductWhitUserId, IMapper mapper)
        {
            _getProductWhitUserId = getProductWhitUserId;
            _mapper = mapper;
        }

        public async Task<List<Product>> Handle(ShowProductWhitUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getProductWhitUserId.GetProducts(request.UserId);
            return result;
        }
    }
}
