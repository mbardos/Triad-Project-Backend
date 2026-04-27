using System.ComponentModel.DataAnnotations.Schema;

namespace TriadInterviewBackend.DataLayer.Entities
{
    [Table("TB_PROJECT")]
    public class ProjectEntity
    {
        [Column("PROJ_ID")]
        public int Id { get; set; }

        [Column("PROJ_NAME")]
        public required string Name { get; set; }

        [Column("PROJ_DESC")] 
        public required string Description { get; set; }

        [Column("PROJ_CREATED_BY_USER_NAME")]
        public required string CreatedByUserName { get; set; }

        [Column("PROJ_EDIT_BY_USER_NAME")]
        public string? EditByUserName { get; set; }
    }
}
