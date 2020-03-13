namespace OutdoorShop.Catalog.Api.Categories
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("categories")]
    [ApiController]
    [ApiVersion("1.0")]
    public class Controller: ControllerBase
    {
        private readonly IMediator mediator;

        public Controller(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<GetAll.Model>>> GetAllAsync()
        {
            var categories = await mediator.Send(new GetAll.Query());

            return Ok(categories.ToList());
        }
    }
}
