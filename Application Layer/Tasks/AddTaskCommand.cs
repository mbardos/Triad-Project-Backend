using MediatR;

public class AddTaskCommand : IRequest<bool>
{
    public TaskDto Task { get; set; }

    public AddTaskCommand(TaskDto task)
    {
        Task = task;
    }

    public class Handler : IRequestHandler<AddTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;

        public Handler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var projectId = request.Task.ProjectId == 0 ? null : request.Task.ProjectId;
            var task = new ProjectTask
            {
                Name = request.Task.Name,
                Description = request.Task.Description,
                ProjectId = projectId,
                State = request.Task.State,
                CreatedUserId = request.Task.CreatedUserId,
                EditedUserId = request.Task.EditedUserId
            };

            Console.WriteLine($"Adding Task: Name={task.Name}, Description={task.Description}, ProjectId={task.ProjectId}, State={task.State}, CreatedUserId={task.CreatedUserId}, EditedUserId={task.EditedUserId}");

            return await _taskRepository.AddTaskAsync(task);
        }
    }
}