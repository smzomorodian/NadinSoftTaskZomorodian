using Application.NadinSoft.Query;
using Domain.NadinSoft.Interface;
using Domain.NadinSoft.Model;
using MapsterMapper;
using MediatR;

namespace Application.NadinSoft.QueryHandler
{
    public class ShowProductWhitUserIdQueryHandler : IRequestHandler<ShowProductWhitUserIdQuery, List<Product>>
    {
        private readonly IGetProductWhitUserIdRepository _getProductWhitUserId;
        private IMapper _mapper;
        public ShowProductWhitUserIdQueryHandler(IGetProductWhitUserIdRepository getProductWhitUserId, IMapper mapper)
        {
            _getProductWhitUserId = getProductWhitUserId;
            _mapper = mapper;
        }

        public async Task<List<Product>> Handle(ShowProductWhitUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getProductWhitUserId.GetProductsWhitUserId(request.UserId);
            return result;
        }
    }
}
