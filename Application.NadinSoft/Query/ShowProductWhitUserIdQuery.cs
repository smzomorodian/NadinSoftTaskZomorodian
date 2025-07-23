using Domain.NadinSoft.Model;
using MediatR;


namespace Application.NadinSoft.Query
{
    public class ShowProductWhitUserIdQuery : IRequest<List<Product>>
    {
        public string UserId { get; set; }
    }
}
