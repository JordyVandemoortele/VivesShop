using VivesShop.Models;
using VivesShop.Core;

namespace VivesShop.Services
{
    public class ProductCategoryService
    {
        private readonly VivesShopDbContext _dbContext;

        public ProductCategoryService(VivesShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<ProductCategory> Find()
        {
            return _dbContext.ProductCategories
                .ToList();
        }

        //Get by id
        public ProductCategory? Get(int id)
        {
            return _dbContext.ProductCategories.Find(id);
        }

        //Create
        public ProductCategory? Create(ProductCategory category)
        {
            _dbContext.Add(category);
            _dbContext.SaveChanges();

            return category;
        }

        //Update
        public ProductCategory? Update(int id, ProductCategory category)
        {
            var dbCategory = _dbContext.ProductCategories.Find(id);
            if (dbCategory is null)
            {
                return null;
            }

            dbCategory.Name = category.Name;
            _dbContext.SaveChanges();
            return dbCategory;
        }

        //Delete
        public void Delete(int id)
        {
            var person = new ProductCategory
            {
                Id = id,
                Name = string.Empty
            };
            _dbContext.ProductCategories.Attach(person);

            _dbContext.ProductCategories.Remove(person);

            _dbContext.SaveChanges();
        }
    }
}
