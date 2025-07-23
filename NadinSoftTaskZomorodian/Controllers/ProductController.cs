using Application.NadinSoft.Command.ProductCommand;
using Application.NadinSoft.DTO.ProductDTO;
using Application.NadinSoft.Query;
using Mapster;
using MediatR;
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
        [HttpGet("All_Product")]
        public async Task<IActionResult> GEtAllProduct()
        {
            var result = await _mediator.Send(new ShowAllProductQuery());
            return Ok(result);
        }

        [HttpGet("Get_Product_whitUserId")]
        public async Task<IActionResult> GeProductwhitUserId(string UserId)
        {
            var command = new ShowProductWhitUserIdQuery { UserId = UserId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[Authorize]
        [HttpPost("Add_product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO addProductDTO)
        {
            var command = addProductDTO.Adapt<AddProductCommand>();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        //[Authorize]
        [HttpDelete("Delete_Product")]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeletedProductDTO deletedProductDTO)
        {
            var command = deletedProductDTO.Adapt<DeletedProductCommand>();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
