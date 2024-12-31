using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Getting_params.Controllers
{
    public class Product()
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        static List<Product> products = new List<Product>();
        [HttpPost]
        //Create
        public List<Product>? CreateProduct([FromBody] List<Product> prod)
        {
            int count =prod.Count;
            foreach (var item in prod)
            {
                item.Id = ++count;
                products.Add(item);
            }
            return products;
        }
        [HttpGet("{Id:int}")]
        //Get
        public Product? GetProducts(int Id)
        {
            foreach (var item in products)
            {
               if (item.Id == Id)
                {
                    return item;
                }
            }
            return null;
        }
        [HttpDelete("{Id:int}")]
        //Delete
        public List<Product>? DeleteProducts(int Id)
        {
            products.Remove(products[Id-1]);
            return products;
        }
        [HttpPut]
        //Update
        public Product? UpdateProduct([FromBody] Product product)
        {
            Product NewProduct = null;
            foreach (var item in products)
            {
                if (item.Id == product.Id)
                {
                    NewProduct= item;
                    item.Id = product.Id;
                    item.Name = product.Name;
                    item.Price = product.Price;
                    break;
                }
            }
                return NewProduct;
        }
    }
}
