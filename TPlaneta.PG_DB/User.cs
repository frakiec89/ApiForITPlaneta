using System.ComponentModel.DataAnnotations;

namespace TPlaneta.PG_DB
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }   
        public string Password { get; set; }
        public string Email { get; set; }
        public Role? Role { get; set; }
        public int? RoleId { get; set; } 
    }
}