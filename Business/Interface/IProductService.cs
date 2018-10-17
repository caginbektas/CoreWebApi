using System;
using System.Collections.Generic;
using System.Text;
using Data.DTOs;
using Data.Entity;

namespace Business.Interface
{
    public interface IProductService
    {
        ServiceResult<List<ProductDTO>> GetAllProducts();
        ServiceResult<ProductDTO> GetProductById(int id);
        ServiceResult<bool> UpdateProductById(int id, string description);
    }
}
