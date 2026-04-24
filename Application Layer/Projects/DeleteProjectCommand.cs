using MediatR;

public class DeleteProjectCommand : IRequest<bool>
{
    public int ProjectId { get; set; }

    public DeleteProjectCommand(int projectId)
    {
        ProjectId = projectId;
    }

    public class Handler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;
    
        public Handler(IProjectRepository projectRepository, IMediator mediator)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
        }
    
        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var existingProject = await _projectRepository.GetProjectByIdAsync(request.ProjectId);
            if (existingProject == null)
            {
                return false; // Project not found
            }

            var updateTasksResult = await _mediator.Send(new UpdateTaskRemoveProjectIdCommand(request.ProjectId));

            if (!updateTasksResult)
            {
                return false; // Failed to update tasks
            }
            
            return await _projectRepository.DeleteProjectAsync(request.ProjectId);
        }
    }
}