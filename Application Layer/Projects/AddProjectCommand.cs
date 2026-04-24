using MediatR;

public class AddProjectCommand : IRequest<bool>
{

    public ProjectDto Project { get; set; }

    public AddProjectCommand(ProjectDto project)
    {
        Project = project;
    }

    public class Handler : IRequestHandler<AddProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public Handler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDto = request.Project;
            if (await _projectRepository.GetProjectByNameAsync(projectDto.Name) != null)
            {
                return false; // Project with the same name already exists  
            }

            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                CreatedUserId = projectDto.CreatedUserId,
                EditedUserId = projectDto.EditedUserId
            };

            return await _projectRepository.AddProjectAsync(project);
        }
    }
}