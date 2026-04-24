using Microsoft.EntityFrameworkCore;

public class TaskRepository : ITaskRepository
{
    private readonly TriadDbContext _context;

    public TaskRepository(TriadDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectTask> GetTaskByIdAsync(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

        if (task == null)
            return null;
            
        return EntityToDomain(task);
    }

    public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
    {
        var tasks = await _context.Tasks.ToListAsync();
        var taskList = new List<ProjectTask>();

        foreach (var t in tasks){
            taskList.Add(EntityToDomain(t));
        }
        return taskList;
    }

    public async Task<bool> AddTaskAsync(ProjectTask taskToAdd)
    {
        var taskEntity = DomainToEntity(taskToAdd);
        Console.WriteLine($"Adding Task: Name={taskEntity.Name}, Description={taskEntity.Description}, ProjectId={taskEntity.ProjectId}, State={taskEntity.State}, CreatedByUserId={taskEntity.CreatedByUserId}, EdittedByUserId={taskEntity.EdittedByUserId}");
        try
        {
            await _context.Tasks.AddAsync(taskEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return false;       
        }
        return true;
    }

    public async Task<bool> UpdateTaskAsync(ProjectTask taskToUpdate)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskToUpdate.Id);

        task.Name = taskToUpdate.Name;
        task.Description = taskToUpdate.Description;
        task.State = taskToUpdate.State;
        task.ProjectId = taskToUpdate.ProjectId;
        task.CreatedByUserId = taskToUpdate.CreatedUserId;
        task.EdittedByUserId = taskToUpdate.EditedUserId;

        try
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return false;       
        }
        return true;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

        try
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return false;
        }
        return true;
    }

    public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId)
    {
        var tasks = await _context.Tasks.Where(x => x.ProjectId == projectId).ToListAsync();
        var taskList = new List<ProjectTask>();

        foreach (var t in tasks){
            taskList.Add(EntityToDomain(t));
        }
        return taskList;
    }

    public async Task<bool> RemoveDeletedProjectIdAsync(int projectId)
    {
        var tasks = await _context.Tasks.Where(x => x.ProjectId == projectId).ToListAsync();

        try
        {
            if (tasks.Count == 0)
                return true; // No tasks to update
            foreach (var task in tasks)
            {
                task.ProjectId = null;
                _context.Tasks.Update(task);
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return false;
        }
        return true;
    }
    private ProjectTask EntityToDomain(TaskEntity taskEntity)
    {
        return new ProjectTask
        {
            Id = taskEntity.Id,
            Name = taskEntity.Name,
            Description = taskEntity.Description,
            State = taskEntity.State,
            ProjectId = taskEntity.ProjectId,
            CreatedUserId = taskEntity.CreatedByUserId,
            EditedUserId = taskEntity.EdittedByUserId
        };
    }

    private TaskEntity DomainToEntity(ProjectTask task)
    {
        return new TaskEntity
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            State = task.State,
            ProjectId = task.ProjectId,
            CreatedByUserId = task.CreatedUserId,
            EdittedByUserId = task.EditedUserId
        };
    }
}