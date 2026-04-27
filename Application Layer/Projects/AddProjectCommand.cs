using MediatR;
using TriadInterviewBackend.DomainLayer.Aggregates;
using TriadInterviewBackend.DomainLayer.Contracts;
using TriadInterviewBackend.ApplicationLayer.DTOs;

namespace TriadInterviewBackend.ApplicationLayer.Projects
{
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
                    CreatedUserName = projectDto.CreatedUserName,
                    EditedUserName = projectDto.EditedUserName
                };

                return await _projectRepository.AddProjectAsync(project);
            }
        }
    }
}
