using System.ComponentModel.DataAnnotations;

namespace Ganz.Domain.Enttiies.Identity
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public virtual Permission Permission { get; set; }
        public int PermissionId { get; set; }
    }
}
