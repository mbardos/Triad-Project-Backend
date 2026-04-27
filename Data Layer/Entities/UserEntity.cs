using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TriadInterviewBackend.DataLayer.Entities
{
    [Table("TB_USER")]
    public class UserEntity : IdentityUser
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
}

