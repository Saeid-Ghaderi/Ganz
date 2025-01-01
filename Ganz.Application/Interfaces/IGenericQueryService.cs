using Ganz.Application.Dtos;
using Ganz.Application.Utilities;

namespace Ganz.Application.Interfaces
{
    public interface IGenericQueryService<T> where T : class
    {
        Task<GeneralActionResult<List<T>>> QueryAsync(GridQueryDto args, IList<string> fields = null, IList<string> includes = null);
    }
}
