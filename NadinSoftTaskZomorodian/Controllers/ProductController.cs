using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.DTO;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NadinSoftTaskZomorodian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost ("Add product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO addProductDTO)
        {
            var command = addProductDTO.Adapt<AddProductCommand>();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
