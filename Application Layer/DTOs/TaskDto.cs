namespace TriadInterviewBackend.ApplicationLayer.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int? ProjectId { get; set; }
        public required string State { get; set; }
        public required string CreatedUserName { get; set; }
        public string? EditedUserName { get; set; }

    }
}
