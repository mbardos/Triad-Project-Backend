using MediatR;
using TriadInterviewBackend.ApplicationLayer.DTOs;
using TriadInterviewBackend.DomainLayer.Contracts;

namespace TriadInterviewBackend.ApplicationLayer.Tasks
{
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
                        CreatedUserName = task.CreatedUserName,
                        EditedUserName = task.EditedUserName
                    };
                    taskList.Add(taskDto);
                }

                return taskList;
            }
        }
    }
}
