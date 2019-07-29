using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WishList.Services.Models.Common;
using WishList.Repositories.ReadOnly.Interfaces;
using WishList.Services.Interfaces;
using WishList.Models.Product;
using WishList.Controllers;

namespace WishList.API.CustomersAndProducts.Controllers
{
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;
        private readonly IProductQueryRepository productQueryRepository;

        public ProductsController(IProductService productService, IProductQueryRepository productQueryRepository)
        {
            this.productService = productService;
            this.productQueryRepository = productQueryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts([FromQuery] PaginationModel paginationModel)
        {
            return Ok(await this.productQueryRepository.GetAll(paginationModel));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var model = await this.productQueryRepository.GetById(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreationModel productModel)
        {
            if (productModel == null)
                return BadRequest();

            var model = await this.productService.Create(productModel);

            if (productService.HasNotifications)
                return CreateResponse(productService.Notifications);
                        
            return CreatedAtAction(nameof(GetProduct), new { id = model.Id }, model);
        }
    }
}
