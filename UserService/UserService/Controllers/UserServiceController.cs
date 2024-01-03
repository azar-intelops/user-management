using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.UserService;
using Application.Queries.UserService;
using Application.Responses;
using System.Net;


namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserServiceController> _logger;
        public UserServiceController(IMediator mediator, ILogger<UserServiceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        
        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        

        

        
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserResponse>> GetUserById(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User GET request received for ID {id}", id);
            var response = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return Ok(response);
        }
        

        

        
    }
}
