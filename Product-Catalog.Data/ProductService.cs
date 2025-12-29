using Product_Catalog.Models;
using Product_Catalog.Models.Entities;

namespace Product_Catalog.Data
{
    public class ProductService : IProductService
    {
        private ProductContext _productDbContext;

        public ProductService(ProductContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return _productDbContext.Products
            .Where(product => product.Active)
            .Select(
                product => new ProductDto {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = ""
            }).ToList();
        }

        public ProductDto GetProductDetails(int productId)
        {
            //TODO: Change to report not found error.
            Product? product = _productDbContext.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null)
            {
                throw new Exception("Error: Product not found");
            }
            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = ""
            };
        }

        //Editing record or adding
        public async Task SetProductAsync(ProductDto productDto)
        {
            Product? existingProduct = _productDbContext.Products.FirstOrDefault(x => x.Id == productDto.Id);
            
            if (existingProduct != null)
            {
                existingProduct.ProductName = productDto.ProductName;
                existingProduct.Price = productDto.Price;
                existingProduct.Description = productDto.Description;
                existingProduct.CategoryId = productDto.CategoryId;
            } else
            {
                Product product = new Product
                {
                    ProductName = productDto.ProductName,
                    Price = productDto.Price,
                    Description = productDto.Description,
                    CategoryId = productDto.CategoryId,
                    Active = true
                };
                _productDbContext.Products.Add(product);
            }
            await _productDbContext.SaveChangesAsync();
        }

        //Returns: If successful.
        public async Task<bool> ToggleActiveAsync(int productId)
        {
            Product? existingProduct = _productDbContext.Products.FirstOrDefault(x => x.Id == productId);
            if (existingProduct != null)
            {
                existingProduct.Active = !existingProduct.Active;
                await _productDbContext.SaveChangesAsync();
                return true;
            } else
            {
                return false;
            }
        }
    }
}
