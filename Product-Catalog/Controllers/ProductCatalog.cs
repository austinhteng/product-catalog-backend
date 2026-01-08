using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Catalog.Models;
using Product_Catalog.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace Product_Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCatalog : ControllerBase
    {
        private IProductService _productService;
        private readonly ILogger<ProductCatalog> _logger;

        public ProductCatalog(IProductService productService, ILogger<ProductCatalog> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        //[Route("api/GetProducts")]
        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Policy = "North America")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            IEnumerable<ProductDto> products = _productService.GetProducts();
            return (products != null) ? Ok(products) : NotFound("Products not found.");
        }

        [HttpGet("GetProductDetails/{productId}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public ActionResult<ProductDto> GetProductDetails(int productId)
        {
            try
            {
                ProductDto foundProduct = _productService.GetProductDetails(productId);
                return Ok(foundProduct);
            }
            catch
            {
                return NotFound("Product of ID " + productId + " not found.");
            }
        }

        [HttpPost("PostProduct")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        public async Task<ActionResult> PostProduct([FromBody] ProductDto productDto)
        {
            //if model state is valid
            //Validate productDto
            if (ModelState.IsValid)
            {
                await _productService.SetProductAsync(productDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("ToggleActive/{productId:int}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        public async Task<ActionResult> ToggleActiveAsync([FromRoute] int productId)
        {
            if (await _productService.ToggleActiveAsync(productId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
