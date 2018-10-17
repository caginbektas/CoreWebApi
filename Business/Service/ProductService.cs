using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using Business.Interface;
using Data.DTOs;

namespace Business.Service
{
    public class ProductService : IProductService
    {

        public IProductRepsitory ProductRepository { get; set; }


        public ProductService(IProductRepsitory ProductRepository)
        {
            this.ProductRepository = ProductRepository;
        }

        public ServiceResult<List<ProductDTO>> GetAllProducts()
        {
            List<Product> products = ProductRepository.GetAllProducts();
            List<ProductDTO> result = new ProductDTO().Clone(products);

            return result.Count() > 0 ? new ServiceResult<List<ProductDTO>>(result) : new ServiceResult<List<ProductDTO>>(new Exception("There is no product"));
        }

        public ServiceResult<bool> UpdateProductById(int id, string description)
        {
            string errorMessage = null;
            Product product = ProductRepository.GetProductById(id);

            if (product != null)
            {
                bool validateProduct = ValidateEntity(product, out errorMessage);

                if (!validateProduct)
                    return new ServiceResult<bool>(new Exception("Update Product fail => " + errorMessage));

                int result = ProductRepository.UpdateProductDescription(product, description);


                if (result > 0)
                    return new ServiceResult<bool>(true);
                else
                    return new ServiceResult<bool>(new Exception("There is nothing to be updated"));
            }
            else
                return new ServiceResult<bool>(new Exception("There is no product with specified ID"));
        }

        public ServiceResult<ProductDTO> GetProductById(int id)
        {
            Product product = ProductRepository.GetProductById(id);
            ProductDTO result = new ProductDTO().Clone(product);
            return result != null ? new ServiceResult<ProductDTO>(result) : new ServiceResult<ProductDTO>(new Exception("There is no product with specified ID"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product">required - Product Object which will be validated</param>
        /// <param name="errorMessage">out parameter to inform messages</param>
        /// <returns></returns>
        public bool ValidateEntity(Product product, out string errorMessage)
        {
            try
            {
                errorMessage = null;
                var validationContext = new ValidationContext(product);
                Validator.ValidateObject(product, validationContext, validateAllProperties: true);
                return true;
            }
            catch (ValidationException e)
            {
                errorMessage = e.Message;
                return false;
            }

        }
    }
}
