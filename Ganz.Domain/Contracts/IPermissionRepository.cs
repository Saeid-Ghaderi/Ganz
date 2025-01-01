namespace Ganz.Domain.Contracts
{
    public interface IPermissionRepository
    {
        Task<bool> CheckPermission(Guid userId, string permissionFlag);
    }
}
