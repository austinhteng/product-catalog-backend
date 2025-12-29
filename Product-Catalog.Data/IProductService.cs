using Product_Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Catalog.Data
{
    public interface IProductService
    {
        //Note: Do these not need to be async?
        public IEnumerable<ProductDto> GetProducts();
        public ProductDto GetProductDetails(int productId);
        public Task SetProductAsync(ProductDto product);
        public Task<bool> ToggleActiveAsync(int productId);
    }
}
