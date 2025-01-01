using Ganz.Application.Dtos.Catalogs.Features;

namespace Ganz.Application.Interfaces.Catalogs.Features
{
    public interface IFeatureService
    {
        Task<List<FeatureDto>> GetAll();
        Task<FeatureDto> GetById(Guid id);
        Task Add(FeatureDto model);
        Task Edit(FeatureDto model);
        Task Remove(Guid id);

    }
}
