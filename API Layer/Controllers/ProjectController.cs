using Microsoft.AspNetCore.Mvc;
using MediatR;
using TriadInterviewBackend.ApplicationLayer.Projects;
using TriadInterviewBackend.ApplicationLayer.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TriadInterviewBackend.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace TriadInterviewBackend.API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController: ControllerBase
    {
        private readonly IMediator _mediator; 
        private readonly UserManager<IdentityUserEntity> _userManager;

        public ProjectController(IMediator mediator, UserManager<IdentityUserEntity> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));
            return project != null ? Ok(project) : NotFound();
        }

        [HttpGet("projects")]
        public async Task<IActionResult> ListProjects()
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery());
            return Ok(projects);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddProject([FromBody] ProjectDto project)
        {
            var result = await _mediator.Send(new AddProjectCommand(project));
            return result ? Ok() : BadRequest();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectDto project)
        {
            var result = await _mediator.Send(new UpdateProjectCommand(project));
            return result ? Ok() : BadRequest();
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));
            return result ? Ok() : NotFound();
        }
    }
}
