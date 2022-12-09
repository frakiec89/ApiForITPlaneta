using System.ComponentModel.DataAnnotations;

namespace TPlaneta.PG_DB
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}