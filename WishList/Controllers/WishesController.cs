using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WishList.Controllers;
using WishList.Models.Wish;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Services.Models.Common;

namespace WishList.API.CustomersAndProducts.Controllers
{
    [Route("api/wishes/{userId}")]
    public class WishesController : BaseController
    {
        private readonly IWishService wishService;
        private readonly IWishQueryRepository wishQueryRepository;

        public WishesController(
            IWishService wishService,
            IWishQueryRepository wishQueryRepository)
        {
            this.wishService = wishService;
            this.wishQueryRepository = wishQueryRepository;
           
        }

        [HttpGet]
        public async Task<IActionResult> ListWishesFromUser(int userId, [FromQuery] PaginationModel paginationModel)
        {
            return Ok(await this.wishQueryRepository.GetAll(userId, paginationModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWishesFromUser(int userId, [FromBody] IEnumerable<WishCreationModel> wishCreationModel)
        {
            if (wishCreationModel == null)
                return BadRequest();

            await this.wishService.Save(userId, wishCreationModel);

            if (wishService.HasNotifications)
                return CreateResponse(wishService.Notifications);

            return CreatedAtAction(nameof(ListWishesFromUser), new { userId }, null);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteWishesFromUser(int userId, int productId)
        {
            await this.wishService.Remove(userId, productId);

            if (wishService.HasNotifications)
                return CreateResponse(wishService.Notifications);

            return Ok();
        }
    }
}
