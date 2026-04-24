public interface IProjectRepository
{
    Task<Project> GetProjectByIdAsync(int id);
    Task<Project> GetProjectByNameAsync(string name);
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<bool> AddProjectAsync(Project project);
    Task<bool> UpdateProjectAsync(Project project);
    Task<bool> DeleteProjectAsync(int id);
}