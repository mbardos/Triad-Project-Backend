using MediatR;
using TriadInterviewBackend.DomainLayer.Aggregates;
using TriadInterviewBackend.DomainLayer.Contracts;
using TriadInterviewBackend.ApplicationLayer.DTOs;

namespace TriadInterviewBackend.ApplicationLayer.Tasks
{
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
                    CreatedUserName = request.Task.CreatedUserName,
                    EditedUserName = request.Task.EditedUserName
                };

                return await _taskRepository.AddTaskAsync(task);
            }
        }
    }
}
