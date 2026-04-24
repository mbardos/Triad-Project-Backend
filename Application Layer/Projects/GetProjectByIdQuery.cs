using MediatR;

public class GetProjectByIdQuery : IRequest<ProjectDto>
{
    public int ProjectId { get; set; }

    public GetProjectByIdQuery(int projectId)
    {
        ProjectId = projectId;
    }

    public class Handler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IProjectRepository _projectRepository;

        public Handler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.ProjectId);
            if (project == null)
            {
                return null;
            }

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedUserId = project.CreatedUserId,
                EditedUserId = project.EditedUserId
            };
        }
    }
}