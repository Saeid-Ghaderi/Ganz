using System.ComponentModel.DataAnnotations;

namespace Ganz.Domain.Enttiies.Identity
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
    }
}
