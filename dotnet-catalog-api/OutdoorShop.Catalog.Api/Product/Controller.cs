namespace OutdoorShop.Catalog.Api.Product
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("products")]
    [ApiController]
    [ApiVersion("1.0")]
    public class Controller : ControllerBase
    {
        private readonly IMediator mediator;

        public Controller(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get a specific product by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GetById.Model>> GetByIdAsync(string id)
        {
            var product = await mediator.Send(new GetById.Query {Id = long.Parse(id)});

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
    }
}