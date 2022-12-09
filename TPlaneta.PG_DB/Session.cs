using System.ComponentModel.DataAnnotations;

namespace TPlaneta.PG_DB
{
    public class Session
    {
        [Key]

        public int SessionId { get; set; }
        public string Tokken { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}