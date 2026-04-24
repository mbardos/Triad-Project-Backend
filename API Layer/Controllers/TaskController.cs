using Microsoft.AspNetCore.Mvc;
using MediatR;

[Route("api/[controller]")]
[ApiController]
public class TaskController: ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id));
        return task != null ? Ok(task) : NotFound();
    }

    [HttpGet("tasks")]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _mediator.Send(new GetAllTasksQuery());
        return tasks != null ? Ok(tasks) : NotFound();;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddTask([FromBody] TaskDto task)
    {
        Console.WriteLine($"Received Task: Name={task.Name}, Description={task.Description}, ProjectId={task.ProjectId}, State={task.State}, CreatedUserId={task.CreatedUserId}, EditedUserId={task.EditedUserId}");    
        var result = await _mediator.Send(new AddTaskCommand(task));
        return result ? Ok() : BadRequest();
    }

    [HttpPut("edit")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskDto task)
    {
        var result = await _mediator.Send(new UpdateTaskCommand(task));
        return result ? Ok() : BadRequest();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _mediator.Send(new DeleteTaskCommand(id));
        return result ? Ok() : NotFound();
    }
}