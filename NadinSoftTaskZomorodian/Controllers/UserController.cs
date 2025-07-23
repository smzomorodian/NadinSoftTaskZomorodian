using Application.NadinSoft.Command.User;
using Application.NadinSoft.DTO.UserDTO;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NadinSoftTaskZomorodian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("Register User")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var command = dto.Adapt<RegisterUserCommand>();

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("Login User")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO dto)
        {
            var command = dto.Adapt<LoginUserCommand>();

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
