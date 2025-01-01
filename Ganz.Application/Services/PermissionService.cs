using Ganz.Application.Interfaces;
using Ganz.Domain.Contracts;

namespace Ganz.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<bool> CheckPermission(Guid userId, string permissionFlag)
        {
            var result = await _permissionRepository.CheckPermission(userId, permissionFlag);
            return result;
        }
    }
}
