using System;
using System.Collections.Generic;
using Business;
using Xunit;
using Data.Entity;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Data.DTOs;
using System.Reflection;
using Data.Model;

namespace UnitTests
{
    public class ServiceControllerTest : TestBase
    {
        [Fact]
        public void GetAllProducts_Test_Success()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.GetAllProducts()).Returns(productList);
            var actionResult = serviceController.GetAllProducts();
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            Type objType = okObjectResult.GetType();

            PropertyInfo statusProp1 = objType.GetProperty("Value");
            IList products = (IList)statusProp1.GetValue(okObjectResult, null);
            PropertyInfo statusProp2 = products[0].GetType().GetProperty("Id");
            var firstProductsId = (int)(statusProp2.GetValue(products[0], null));

            PropertyInfo statusProp = objType.GetProperty("StatusCode");
            var statusValue = (int)(statusProp.GetValue(okObjectResult, null));

            Assert.AreEqual<int>(statusValue, 200);
            Assert.AreEqual<int>(firstProductsId, productList.First().Id);
        }
        [Fact]
        public void GetAllProducts_Test_Fail()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.GetAllProducts()).Returns(new List<Product>());
            var actionResult = serviceController.GetAllProducts();
            var noContentResult = actionResult as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }
        [Fact]
        public void GetProductById_Test_NoProduct()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.GetProductById(1)).Returns((Product)null);
            var actionResult = serviceController.GetProductById(1);
            var noContentResult = actionResult as NoContentResult;
            Assert.IsNotNull(noContentResult);
        }
        [Fact]
        public void GetProductById_Test_WithProduct()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.GetProductById(1)).Returns(product);
            var actionResult = serviceController.GetProductById(1);
            var okObjectResult = actionResult as OkObjectResult;

            Type objectType = okObjectResult.Value.GetType();

            PropertyInfo dataProp = objectType.GetProperty("Data");
            var dataValue = (ProductDTO)(dataProp.GetValue(okObjectResult.Value, null));

            PropertyInfo statusProp = objectType.GetProperty("ResultType");
            var statusValue = (ServiceResultType)(statusProp.GetValue(okObjectResult.Value, null));

            Assert.AreEqual<ServiceResultType>(statusValue, ServiceResultType.Success);
            Assert.AreEqual<int>(dataValue.Id, product.Id);
        }
        [Fact]
        public void UpdateProductById_Test_NoProduct()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.UpdateProductDescription(product, "newDesc")).Returns(0);
            mockedProductRepository.Setup(a => a.GetProductById(1)).Returns(product);
            var actionResult = serviceController.UpdateProductById(new ProductModel { Id = 1, Description = "newDesc" });
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
        }
        [Fact]
        public void UpdateProductById_Test_NotValidate()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.UpdateProductDescription(product, "newDesc")).Returns(0);
            product.Name = null;
            mockedProductRepository.Setup(a => a.GetProductById(1)).Returns(product);
            var actionResult = serviceController.UpdateProductById(new ProductModel { Id = 1, Description = "newDesc" });
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value.ToString().StartsWith("Update Product fail"));
        }
        [Fact]
        public void UpdateProductById_Test_NothingToUpdate()
        {
            TestInitialize();
            mockedProductRepository.Setup(a => a.UpdateProductDescription(product, "newDesc")).Returns(0);
            mockedProductRepository.Setup(a => a.GetProductById(1)).Returns(product);
            var actionResult = serviceController.UpdateProductById(new ProductModel { Id = 1, Description = "newDesc" });
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(badRequestResult.Value, "There is nothing to be updated");
        }
    }
}
