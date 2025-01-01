using Ganz.Application.CQRS.ProductCommandQuery.Command;
using Ganz.Application.CQRS.ProductCommandQuery.Query;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Ganz.API.Controllers
{
    public class ProductCQRSController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductCQRSController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductCommand saveProductCommand)
        {
            var result = await _mediator.Send(saveProductCommand);
            return Ok(result);
        }

        [HttpGet("GetProductsCQRS")]
        public async Task<IActionResult> GetProductsCQRS([FromQuery]GetProductQuery getProductQuery)
        {
            var result = await _mediator.Send(getProductQuery);

            return Ok(result);
        }
    }
}
