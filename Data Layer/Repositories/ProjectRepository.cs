using Microsoft.EntityFrameworkCore;

public class ProjectRepository : IProjectRepository
{
    private readonly TriadDbContext _context;

    public ProjectRepository(TriadDbContext context)
    {
        _context = context;
    }

    public async Task<Project> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

        if (project == null)
            return null;
            
        return EntityToAggregate(project);
    }

    public async Task<Project> GetProjectByNameAsync(string name)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

        if (project == null)
            return null;
            
        return EntityToAggregate(project);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var projects = await _context.Projects.ToListAsync();
        var projectList = new List<Project>();

        foreach (var proj in projects){
            projectList.Add(EntityToAggregate(proj));
        }
        return projectList;
    }

    public async Task<bool> AddProjectAsync(Project projectToAdd)
    {
        var projectEntity = AggregateToEntity(projectToAdd);

        try
        {
            await _context.Projects.AddAsync(projectEntity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return false;       
        }
        return true;
    }

    public async Task<bool> UpdateProjectAsync(Project projectToUpdate)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectToUpdate.Id);

        project.Name = projectToUpdate.Name;
        project.Description = projectToUpdate.Description;
        project.CreatedByUserId = projectToUpdate.CreatedUserId;
        project.EditByUserId = projectToUpdate.EditedUserId;

        try
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating project: {ex.Message}");
            return false;       
        }
        return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

        try
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return false;       
        }
        return true;
    }
    private Project EntityToAggregate(ProjectEntity projectEntity)
    {
        return new Project
        {
            Id = projectEntity.Id,
            Name = projectEntity.Name,
            Description = projectEntity.Description,
            CreatedUserId = projectEntity.CreatedByUserId,
            EditedUserId = projectEntity.EditByUserId
        };
    }

    private ProjectEntity AggregateToEntity(Project project)
    {
        return new ProjectEntity
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedByUserId = project.CreatedUserId,
            EditByUserId = project.EditedUserId
        };
    }
}