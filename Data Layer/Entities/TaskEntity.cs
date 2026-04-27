using System.ComponentModel.DataAnnotations.Schema;

namespace TriadInterviewBackend.DataLayer.Entities
{
    [Table("TB_TASK")]

    public class TaskEntity
    {
        [Column("TASK_ID")]
        public int Id { get; set; }

        [Column("TASK_NAME")]
        public required string Name { get; set; }

        [Column("TASK_DESC")]
        public required string? Description { get; set; }

        [Column("TASK_STATE")]
        public required string State { get; set; }

        [Column("PROJ_ID")]
        public int? ProjectId { get; set; }

        [Column("TASK_CREATED_BY_USER_NAME")]
        public required string CreatedByUserName { get; set; }

        [Column("TASK_EDITTED_BY_USER_NAME")]
        public string? EdittedByUserName { get; set; }
    }
}
