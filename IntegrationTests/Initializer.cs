using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTests
{
    public class Initializer
    {
        /// <summary>
        /// Adds single product to database in order to automation tests
        /// Every test method has to call it before operations.
        /// </summary>
        /// <returns></returns>
        public int BeforeTest()
        {
            using (var db = new DataContext())
            {
                var product = db.Set<Product>();
                product.Add(new Product()
                {
                    ImgUri = "hhtp//www.a.com/9999.jpg",
                    Name = "TestProduct",
                    Price = 1,
                    Currency = "TRY",
                    Description = "Nice Product"
                });

                db.SaveChanges();
                return db.Product.Last().Id;
            }
        }
        /// <summary>
        /// Deletes the product which is added in BeforeTest()
        /// Every test method has to call in the end of test
        /// </summary>
        /// <param name="id"></param>
        public void AfterTest(int id)
        {
            using (var db = new DataContext())
            {
                var product = db.Product.Where(a => a.Id == id).FirstOrDefault();

                db.Product.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
