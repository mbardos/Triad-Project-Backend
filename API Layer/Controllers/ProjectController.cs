using Microsoft.AspNetCore.Mvc;
using MediatR;

[Route("api/[controller]")]
[ApiController]
public class ProjectController: ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
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
    public async Task<IActionResult> DeleteProject(int id)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id));
        return result ? Ok() : NotFound();
    }
}