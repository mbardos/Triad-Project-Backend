using MediatR;

public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
{
    public class Handler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public Handler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllProjectsAsync();

            var projectList = new List<ProjectDto>();  

            foreach (var project in projects)
            {
                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    CreatedUserId = project.CreatedUserId,
                    EditedUserId = project.EditedUserId
                };
                projectList.Add(projectDto);
            }

            return projectList;
        }
    }
}