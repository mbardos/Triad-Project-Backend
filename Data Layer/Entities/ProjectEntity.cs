using System.ComponentModel.DataAnnotations.Schema;

[Table("TB_PROJECT")]
public class ProjectEntity
{
    [Column("PROJ_ID")]
    public int Id { get; set; }

    [Column("PROJ_NAME")]
    public required string Name { get; set; }

    [Column("PROJ_DESC")] 
    public required string Description { get; set; }

    [Column("PROJ_CREATED_BY_USER_ID")]
    public required int CreatedByUserId { get; set; }

    [Column("PROJ_EDIT_BY_USER_ID")]
    public int? EditByUserId { get; set; }
}