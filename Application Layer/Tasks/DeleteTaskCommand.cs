using MediatR;

public class DeleteTaskCommand: IRequest<bool>
{
    public int Id { get; set; }

    public DeleteTaskCommand(int id)
    {
        Id = id;
    }

    public class Handler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;

        public Handler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (await _taskRepository.GetTaskByIdAsync(request.Id) == null)
            {
                return false; // Task not found
            }

            return await _taskRepository.DeleteTaskAsync(request.Id);
        }
    }
}