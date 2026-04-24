using MediatR;

public class UpdateTaskCommand: IRequest<bool>
{
    public TaskDto Task { get; set; }

    public UpdateTaskCommand(TaskDto task)
    {
        Task = task;
    }

    public class Handler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;

        public Handler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(request.Task.Id);
            
            if (existingTask == null)
            {
                return false; // Task not found
            }

            existingTask.Name = request.Task.Name;
            existingTask.Description = request.Task.Description;
            existingTask.ProjectId = request.Task.ProjectId;
            existingTask.State = request.Task.State;
            existingTask.CreatedUserId = request.Task.CreatedUserId;
            existingTask.EditedUserId = request.Task.EditedUserId;

            return await _taskRepository.UpdateTaskAsync(existingTask);
        }
    }
}