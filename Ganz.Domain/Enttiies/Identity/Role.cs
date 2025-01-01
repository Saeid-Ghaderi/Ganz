using System.ComponentModel.DataAnnotations;

namespace Ganz.Domain.Enttiies.Identity
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }

    }
}
