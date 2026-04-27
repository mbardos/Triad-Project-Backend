using Microsoft.AspNetCore.Mvc;
using MediatR;
using TriadInterviewBackend.ApplicationLayer.Users;
using TriadInterviewBackend.ApplicationLayer.DTOs;

namespace TriadInterviewBackend.API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto user)
        {
            var result = await _mediator.Send(new AddUserCommand(user));
            return result ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            var result = await _mediator.Send(new UpdateUserCommand(user));
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            return result ? Ok() : NotFound();
        }
    }
}