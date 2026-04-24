public class ProjectTask
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int? ProjectId { get; set; }
    public required string State { get; set; }
    public required int CreatedUserId { get; set; }
    public int? EditedUserId { get; set; }

}