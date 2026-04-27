using MediatR;
using TriadInterviewBackend.DomainLayer.Contracts;

namespace TriadInterviewBackend.ApplicationLayer.Tasks
{
    public class UpdateTaskRemoveProjectIdCommand: IRequest<bool>
    {
        public int ProjectId { get; set; }

        public UpdateTaskRemoveProjectIdCommand(int projectId)
        {
            ProjectId = projectId;
        }

        public class Handler : IRequestHandler<UpdateTaskRemoveProjectIdCommand, bool>
        {
            private readonly ITaskRepository _taskRepository;

            public Handler(ITaskRepository taskRepository)
            {
                _taskRepository = taskRepository;
            }

            public async Task<bool> Handle(UpdateTaskRemoveProjectIdCommand request, CancellationToken cancellationToken)
            {
                var updateTasksResult = await _taskRepository.RemoveDeletedProjectIdAsync(request.ProjectId);
                
                return updateTasksResult; // Return the result of updating tasks
            }
        }
    }
}
