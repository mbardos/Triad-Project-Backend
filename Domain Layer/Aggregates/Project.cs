namespace TriadInterviewBackend.DomainLayer.Aggregates
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string CreatedUserName { get; set; }
        public string? EditedUserName { get; set; }
    }
}

