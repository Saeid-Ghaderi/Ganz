using Ganz.Domain;
using Ganz.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Ganz.Infrastructure.Persistence
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public PermissionRepository(ApplicationDBContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public async Task<bool> CheckPermission(Guid userId, string permissionFlag)
        {
            string permissionCacheKey = $"permissions-{userId.ToString()}";
            var permissionFlags = new List<string>();

            //memoryCache.GetOrCreateAsync<List<string>>(permissionCacheKey);

            if (!_memoryCache.TryGetValue(permissionCacheKey, out permissionFlags))
            {
                // Key not in cache, so get data.
                var roles = await _dbContext.UserRoles.Where(q => q.UserId == userId)
               .Select(q => q.RoleId).ToListAsync();

                permissionFlags = await _dbContext.RolePermissions
               .Where(q => roles.Contains(q.RoleId)).Select(q => q.Permission.PermissionFlag).ToListAsync();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                // Save data in cache.
                _memoryCache.Set(permissionCacheKey, permissionFlags, cacheEntryOptions);
            }

            //how to remove from cache with key
            //memoryCache.Remove(permissionCacheKey);

            return permissionFlags.Contains(permissionFlag);
        }
        
    }
}
