using Data.DTOs;
using Data.Entity;
using System.Collections.Generic;

namespace Data.Repository.Interface
{
    public interface IProductRepsitory
    {
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        int UpdateProductDescription(Product product, string newDescription);
    }
}
