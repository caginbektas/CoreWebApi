using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ImgUri is required")]
        public string ImgUri { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductDTO Clone(Product product)
        {
            if (product == null)
                return null;

            this.Id = product.Id;
            this.Name = product.Name;
            this.ImgUri = product.ImgUri;
            this.Currency = product.Currency;
            this.Price = product.Price;
            this.Description = product.Description;
            return this;
        }
        public List<ProductDTO> Clone(List<Product> products)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            foreach (var product in products)
            {
                ProductDTO prd = new ProductDTO();
                prd.Clone(product);
                result.Add(prd);
            }
            return result;
        }
    }
}
