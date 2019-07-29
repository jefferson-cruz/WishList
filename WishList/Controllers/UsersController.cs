using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> ListUsers([FromQuery] PaginationModel paginationModel)
        {
            return Ok(await this.userQueryServiceRepository.GetAll(paginationModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var model = await this.userQueryServiceRepository.GetById(id);

            if (model == null)
                BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] UserCreationModel customer)
        {
            if (customer == null)
                return BadRequest();

            var model = await  this.userService.Create(customer);

            if (this.userService.HasNotifications)
                return CreateResponse(userService.Notifications);

            return CreatedAtAction(nameof(GetUser), new { id = model.Id }, null);
        }
    }
}