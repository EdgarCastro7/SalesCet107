using SalesCet107.Web.Data.Entities;

namespace SalesCet107.Web.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) 
        {
            
        }

    }
}
