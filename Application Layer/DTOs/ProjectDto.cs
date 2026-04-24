public class ProjectDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int CreatedUserId { get; set; }
    public int? EditedUserId { get; set; }
}