using Data.DTOs;
using Data.Entity;
using Data.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository.Service
{
    public class ProductRepository : IProductRepsitory
    {
        public DataContext context { get; set; }
        public ProductRepository()
        {
            if (context == null)
            {
                context = new DataContext();
            }
        }

        public Product GetProductById(int id)
        {
            Product result = new Product();
            using (context)
            {
                result = context.Product.SingleOrDefault(a => a.Id == id);
            }
            return result;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> result = new List<Product>();
            using (context)
            {
                result = context.Product.Select(a => a).ToList();
            }
            return result;
        }

        public int UpdateProductDescription(Product product, string newDescription)
        {
            using (var context = new DataContext())
            {
                product.Description = newDescription;
                context.Product.Update(product);
                return context.SaveChanges();
            }

        }
    }
}
