using System.Collections.Generic;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Business;
using Business.Interface;

namespace CoreWebApi.Controllers
{
    /// <summary>
    /// API Version 1.0
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        IProductService ProductService { get; set; }
        public ServiceController(IProductService ProductService)
        {
            this.ProductService = ProductService;
        }

        /// <summary>
        /// Version 1.0
        /// Using Id parameter to update 
        /// description field of the product
        /// </summary>
        /// <param name="id">Required  - Id of the Product which is going to be updated</param>
        /// <param name="description">Optional - Description, New description of the product</param>
        /// <returns>
        /// OK (200) - Product description updated
        /// BadRequest (400) - No Product with specified ID / Validation Error / Nothing to be updated cases with error message.
        /// </returns>
        [HttpPost]
        [Route("UpdateProductById")]
        public IActionResult UpdateProductById([FromQuery]int id, string description)
        {
            var result = ProductService.UpdateProductById(id, description);
            if (result.Exception == null)
                return Ok(result);
            else
                return BadRequest(result.Exception.Message);
        }

        /// <summary>
        /// Version 1.0
        /// Gets the product with the specified ID
        /// </summary>
        /// <param name="id">Required  Id of the Product which is going to be responsed</param>
        /// <returns>
        /// OK (200) - Product has been successfully responded
        /// NoContent (204) - No Product with specified ID
        /// </returns>
        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetProductById([FromQuery]int id)
        {
            var result = ProductService.GetProductById(id);
            if (result.Data != null)
                return Ok(result);
            else
                return NoContent();
        }

        /// <summary>
        /// Version 1.0
        /// Gets all available products 
        /// </summary>
        /// <returns>
        /// OK (200) - Products has been listed
        /// NoContent (204) - No Product(s) to be shown
        /// </returns>
        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var result = ProductService.GetAllProducts();
            if (result.Data != null && result.Data.Count > 0)
                return Ok(result.Data);
            else
                return NoContent();
        }
    }
}