using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WishList.Controllers;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Services.Models.Common;
using WishList.Services.Models.User;

namespace WishList.API.CustomersAndProducts.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        private readonly IUserQueryRepository userQueryServiceRepository;

        public UsersController(IUserService userService,  IUserQueryRepository userQueryServiceRepository)
        {
            this.userService = userService;
            this.userQueryServiceRepository = userQueryServiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] PaginationModel paginationModel)
        {
            return Ok(await this.userQueryServiceRepository.GetAll(paginationModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await this.userQueryServiceRepository.GetById(id);

            if (model == null)
                NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreationModel customer)
        {
            if (customer == null)
                return BadRequest();

            var model = await  this.userService.Create(customer);

            if (this.userService.HasResults)
                return CreateResponse(userService.Results);

            return CreatedAtAction(nameof(Get), new { id = model.Id }, null);
        }
    }
}