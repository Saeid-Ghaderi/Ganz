using Ganz.Domain;
using Ganz.Domain.Catalogs.Features;
using Ganz.Infrastructure.Base;
using Ganz.Infrastructure.Data;

namespace Ganz.Infrastructure.Persistence.Catalogs.Features
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDBContext _context;

        public FeatureRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Feature> GetById(FeatureId featureId)
        {
            return await _context.Features.FindAsync(featureId);
        }

        public async Task<FeatureId> Insert(Feature feature)
        {
            await _context.AddAsync(feature);
            return feature.Id;
        }

        public async Task Update(Feature feature)
        {
            var currentFeature = await _context.Features.FindAsync(feature.Id);

            if (currentFeature == null) throw new DatabaseException("feature Id in not valid");
            currentFeature.Update(feature);
            //call save changes from UnitOfWork

        }

        public void Delete(FeatureId featureId)
        {
            //1 => get feature from db with featureId
            //====> remove from dbContext
            //======>SaveChanges

            //2 => create newFeature with featureId
            //====> remove from dbContext
            //======>SaveChanges
            var feature = Feature.CreateNewForDelete(featureId);
            _context.Remove(feature);
            //call save changes from UnitOfWork
        }

    }
}
