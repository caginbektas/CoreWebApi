using System;
using Microsoft.VisualStudio;
using Data.Repository.Interface;
using Moq;
using Business;
using Business.Interface;
using Business.Service;
using CoreWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Entity;
using System.Collections.Generic;

namespace UnitTests
{
  
    
    public abstract class TestBase
    {

        protected IProductRepsitory productRepository{get;set;}
        protected IProductService productService { get; set; }
        protected ServiceController serviceController { get; set; }
        protected Product product { get; set; }
        protected List<Product> productList { get; set; }
        protected Mock<IProductRepsitory> mockedProductRepository { get; set; }

        
        public void TestInitialize()
        {
            //Preparing instances to use in unit test methods
            mockedProductRepository = new Mock<IProductRepsitory>();
            productRepository = mockedProductRepository.Object;
            productService = new ProductService(productRepository);
            serviceController = new ServiceController(productService);
            product = new Product();
            product.Id = 1;
            product.ImgUri = "mocked/imageUri/";
            product.Name = "IPhone";
            product.Price = 500;
            product.Currency = "USD";
            product.Description = "Mocked Decription";
            productList = new List<Product>();
            productList.Add(product);
        }
    }
}
