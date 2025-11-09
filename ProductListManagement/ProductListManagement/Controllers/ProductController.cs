using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductListManagement.Service.Contracts;
using ProductListManagement.Service.Dtos;

namespace ProductListManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : AbstractController
    {
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
            : base(logger)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Logger.LogInformation($"ProductController.Get({id}) started");

                var product = await _productService.GetAsync(id);

                Logger.LogInformation($"ProductController.Get({id}) completed");
                return Ok(product);
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"ProductController.Get({id}) completed; invalid request");
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterDto filter)
        {
            try
            {
                Logger.LogInformation($"ProductController.Get started");

                var product = await _productService.GetAsync(filter);

                Logger.LogInformation($"ProductController.Get completed");
                return Ok(product);
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"ProductController.Get completed; invalid request");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductDto productDto)
        {
            try
            {
                Logger.LogInformation("ProductController.Post started");

                await _productService.AddAsync(productDto);

                Logger.LogInformation("ProductController.Post completed");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogWarning("ProductController.Post completed; invalid request");
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductDto productDto)
        {
            try
            {
                Logger.LogInformation("ProductController.Put started");

                var product = await _productService.UpdateAsync(productDto);

                Logger.LogInformation("ProductController.Put completed");
                return Ok(product);
            }
            catch (Exception ex)
            {
                Logger.LogWarning("ProductController.Put completed; invalid request");
                return BadRequest(ex);
            }
        }
    }
}
