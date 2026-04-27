using MediatR;
using TriadInterviewBackend.ApplicationLayer.DTOs;
using TriadInterviewBackend.DomainLayer.Contracts;

namespace TriadInterviewBackend.ApplicationLayer.Projects
{
    public class GetProjectByNameQuery : IRequest<ProjectDto>
    {
        public string ProjectName { get; set; }

        public GetProjectByNameQuery(string projectName)
        {
            ProjectName = projectName;
        }

        public class Handler : IRequestHandler<GetProjectByNameQuery, ProjectDto>
        {
            private readonly IProjectRepository _projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                _projectRepository = projectRepository;
            }

            public async Task<ProjectDto> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
            {
                var project = await _projectRepository.GetProjectByNameAsync(request.ProjectName);
                if (project == null)
                {
                    return null;
                }

                return new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    CreatedUserName = project.CreatedUserName,
                    EditedUserName = project.EditedUserName
                };
            }
        }
    }
}
