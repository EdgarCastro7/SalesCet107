using SalesCet107.Web.Data.Entities;

namespace SalesCet107.Web.Data
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly DataContext _dataContext;
        public CountryRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
