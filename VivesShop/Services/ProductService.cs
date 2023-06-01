using VivesShop.Models;
using VivesShop.Core;
using Microsoft.EntityFrameworkCore;

namespace VivesShop.Services
{
    public class ProductService
    {
        private readonly VivesShopDbContext _dbContext;

        public ProductService(VivesShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Product> Find()
        {
            return _dbContext.Products
                .Include(c => c.Category)
                .ToList();
        }

        //Get by id
        public Product? Get(int id)
        {
            return _dbContext.Products
                .Include(c => c.Category)
                .FirstOrDefault(p => p.Id == id);
        }

        //Create
        public Product? Create(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        //Update
        public Product? Update(int id, Product product)
        {
            var dbProduct = _dbContext.Products.Find(id);
            if (dbProduct is null)
            {
                return null;
            }
            dbProduct.Price = product.Price;
            dbProduct.Category = product.Category;
            dbProduct.CategoryId = product.CategoryId;
            _dbContext.SaveChanges();
            return dbProduct;
        }

        //Delete
        public void Delete(int id)
        {
            var product = new Product
            {
                Price = 0,
                CategoryId = 0,
                Category = null,
                Id = id,
                Name = string.Empty
            };
            _dbContext.Products.Attach(product);

            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();
        }
    }
}
