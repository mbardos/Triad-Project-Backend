using MediatR;

public class GetTaskByIdQuery : IRequest<TaskDto>
{
    public int Id { get; set; }

    public GetTaskByIdQuery(int id)
    {
        Id = id;
    }

    public class Handler : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public Handler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(request.Id);
            
            if (task == null)
            {
                return null; // Task not found
            }

            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                ProjectId = task.ProjectId,
                State = task.State,
                CreatedUserId = task.CreatedUserId,
                EditedUserId = task.EditedUserId
            };
        }
    }
}