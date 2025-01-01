namespace Ganz.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> CheckPermission(Guid userId, string permissionFlag);
    }
}
