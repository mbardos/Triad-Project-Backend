public interface ITaskRepository
{
    Task<ProjectTask> GetTaskByIdAsync(int id);
    Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
    Task<bool> AddTaskAsync(ProjectTask task);
    Task<bool> UpdateTaskAsync(ProjectTask task);
    Task<bool> DeleteTaskAsync(int id);
    Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
    Task<bool> RemoveDeletedProjectIdAsync(int projectId);
}