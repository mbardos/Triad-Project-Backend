using MediatR;

public class GetAllTasksQuery : IRequest<IEnumerable<TaskDto>>
{
    public class Handler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public Handler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllTasksAsync();

            var taskList = new List<TaskDto>();

            foreach (var task in tasks)
            {
                var taskDto = new TaskDto
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    ProjectId = task.ProjectId,
                    State = task.State,
                    CreatedUserId = task.CreatedUserId,
                    EditedUserId = task.EditedUserId
                };
                taskList.Add(taskDto);
            }

            return taskList;
        }
    }
}