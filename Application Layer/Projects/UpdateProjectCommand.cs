using MediatR;
using TriadInterviewBackend.ApplicationLayer.DTOs;
using TriadInterviewBackend.DomainLayer.Contracts;

namespace TriadInterviewBackend.ApplicationLayer.Projects
{
    public class UpdateProjectCommand : IRequest<bool>
    {
        public ProjectDto Project { get; set; }

        public UpdateProjectCommand(ProjectDto project)
        {
            Project = project;
        }

        public class Handler : IRequestHandler<UpdateProjectCommand, bool>
        {
            private readonly IProjectRepository _projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                _projectRepository = projectRepository;
            }

            public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var projectDto = request.Project;
                var existingProject = await _projectRepository.GetProjectByIdAsync(projectDto.Id);
                if (existingProject == null)
                {
                    return false; // Project not found
                }

                var projectWithSameName = await _projectRepository.GetProjectByNameAsync(projectDto.Name);

                if (projectWithSameName != null && projectWithSameName.Id != projectDto.Id)
                {
                    return false; // Another project with the same name already exists
                }

                existingProject.Name = projectDto.Name;
                existingProject.Description = projectDto.Description;
                existingProject.CreatedUserName = projectDto.CreatedUserName;
                existingProject.EditedUserName = projectDto.EditedUserName;

                return await _projectRepository.UpdateProjectAsync(existingProject);
            }
        }
    }
}
