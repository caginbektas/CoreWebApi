using FluentAssertions;
using IntegrationTests.TestBase;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Data.Model;

namespace IntegrationTests.UseCases
{
    public class ProductTest : Initializer
    {
        private readonly TestContext _tester;
        public ProductTest()
        {
            _tester = new TestContext();
        }

        [Fact]
        public async Task GetAllProduct_Test()
        {
            int productId = BeforeTest();

            var result = await _tester.Client.GetAsync("/api/1.0/GetAllProducts");

            result.EnsureSuccessStatusCode();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            AfterTest(productId);
        }

        [Fact]
        public async Task GetProductById_Test_NoProduct()
        {
            int productId = BeforeTest();

            var result = await _tester.Client.GetAsync("/api/1.0/GetProductById?id=" + (productId + 1));

            result.EnsureSuccessStatusCode();
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            AfterTest(productId);
        }

        [Fact]
        public async Task GetProductById_Test_WithProduct()
        {
            int productId = BeforeTest();

            var result = await _tester.Client.GetAsync("/api/1.0/GetProductById?id=" + productId);
            result.EnsureSuccessStatusCode();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            AfterTest(productId);
        }

        [Fact]
        public async Task UpdateProductById_Test_NoProduct()
        {
            int productId = BeforeTest();
            var nonValidProductId = productId + 1;
            
            ProductModel model = new ProductModel()
            {
                Id = nonValidProductId,
                Description = "foo"
            };

            var serializedModel = JsonConvert.SerializeObject(model);

            var bytes = System.Text.Encoding.UTF8.GetBytes(serializedModel);
            var byteContent = new ByteArrayContent(bytes);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var result = await _tester.Client.PostAsync("/api/1.0/UpdateProductById", byteContent);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            AfterTest(productId);
        }

        [Fact]
        public async Task UpdateProductById_Test_WithNoUpdate()
        {
            int productId = BeforeTest();
            var nonValidProductId = productId + 1;

            ProductModel model = new ProductModel()
            {
                Id = nonValidProductId,
                Description = "Nice Product"
            };

            var serializedModel = JsonConvert.SerializeObject(model);

            var bytes = System.Text.Encoding.UTF8.GetBytes(serializedModel);
            var byteContent = new ByteArrayContent(bytes);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var result = await _tester.Client.PostAsync("/api/1.0/UpdateProductById", byteContent);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            AfterTest(productId);
        }

        [Fact]
        public async Task UpdateProductById_Test_Success()
        {
            int productId = BeforeTest();

            ProductModel model = new ProductModel()
            {
                Id = productId,
                Description = "updatedDescription"
            };

            var serializedModel = JsonConvert.SerializeObject(model);

            var bytes = System.Text.Encoding.UTF8.GetBytes(serializedModel);
            var byteContent = new ByteArrayContent(bytes);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await _tester.Client.PostAsync("/api/1.0/UpdateProductById", byteContent);

            AfterTest(productId);
        }
    }
}
