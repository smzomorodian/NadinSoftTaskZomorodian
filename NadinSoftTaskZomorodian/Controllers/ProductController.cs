using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.DTO.ProductDTO;
using Application.NadinSoft.Query;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("All")]
        public async Task<IActionResult> GEtAllProduct()
        {
            var result = await _mediator.Send(new ShowAllProductQuery());
            return Ok(result);
        }

        [HttpGet("Users/{UserId}")]
        public async Task<IActionResult> GeProductwhitUserId([FromRoute] string UserId)
        {
            var command = new ShowProductWhitUserIdQuery { UserId = UserId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductParameter addProductParameter)
        {
            var command = addProductParameter.Adapt<AddProductCommand>();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeletedProducParameter  deletedProducParameter)
        {
            var command = deletedProducParameter.Adapt<DeletedProductCommand>();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("Update_information")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct updateProduct)
        {
            var command = updateProduct.Adapt<UpdateProductCommand>();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
