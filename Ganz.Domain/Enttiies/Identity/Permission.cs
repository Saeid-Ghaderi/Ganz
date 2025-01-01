using System.ComponentModel.DataAnnotations;

namespace Ganz.Domain.Enttiies.Identity
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string? PermissionFlag { get; set; }
        public string? Title { get; set; }

    }
}
