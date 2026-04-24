using System.ComponentModel.DataAnnotations.Schema;

[Table("TB_USER")]
public class UserEntity
{
    [Column("USER_ID")]
    public int Id { get; set; }

    [Column("USER_NAME")]
    public required string Name { get; set; }

    [Column("USER_EMAIL")]
    public required string Email { get; set; }

    [Column("USER_PASSWORD")]
    public required string Password { get; set; }
}