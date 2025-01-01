using Ganz.Application.Dtos.Catalogs.Features;
using Ganz.Application.Interfaces.Catalogs.Features;
using Microsoft.AspNetCore.Mvc;

namespace Ganz.API.Controllers.Catalogs.Features
{
    public class FeatureController : BaseController
    {
        private readonly IFeatureService featureService;

        public FeatureController(IFeatureService featureService)
        {
            this.featureService = featureService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FeatureDto model)
        {
            await featureService.Add(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await featureService.GetById(id);
            return Ok(result);
        }
    }
}
