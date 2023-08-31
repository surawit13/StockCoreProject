using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockCore.Data;
using StockCore.DTOs.Product;
using StockCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Swashbuckle.AspNetCore;
using Mapster;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace StockCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        public ProductController(DatabaseContext databaseContext) => this._dbContext = databaseContext;

        [HttpGet]
        public ActionResult<IEnumerable<ProductResponse>> GetProductALL()
        {
            //var res = _dbContext.Products.ToList();
            //return Ok(res);


            //join with model and select only mapping data
            return _dbContext.Products.Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .Select(ProductResponse.FromProduct).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponse> GetProductById(int id)
        {
            //var res = _dbContext.Products.Find(id);
            //return Ok(res);

            var res = _dbContext.Products.Include(p => p.Category)
                .SingleOrDefault(p => p.ProductId == id);
            //query on ly one data , if null then return null

            if (res == null)
            {
                return NotFound();
            }
            return ProductResponse.FromProduct(res);
        }
        [HttpGet("search")]

        public ActionResult<IEnumerable<ProductResponse>> SearchProduct([FromQuery] string name = "")
        {
            var res = _dbContext.Products.Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Select(ProductResponse.FromProduct).ToList();
            return res;

        }

        [HttpPost]
        public ActionResult<Product> AddProductBy([FromForm] ProductRequest productRequest)
        {
            try
            {
                var product = new Product
                {
                    ProductId = productRequest.ProductId,
                    Name = productRequest.Name,
                    Stock = productRequest.Stock,
                    Price = productRequest.Price,
                    CategoryId = productRequest.CategoryId,
                    //FormFiles = productRequest.FormFilesame
                };

                //var res = ProductRequest.Adapt(Product);


                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                //return StatusCode(201);
                //return Ok();
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateAccount(int id, [FromForm] ProductRequest productRequest)
        {
            if (id != productRequest.ProductId)
            {
                return BadRequest();
            }
            var res = _dbContext.Products.Find(id);

            if (res == null)
            {
                return NotFound();
            }
            //res.Name = productRequest.Name;
            //res.Price = productRequest.Price;
            //res.Stock = productRequest.Stock;
            res.ProductId = productRequest.ProductId;
            res.Name = productRequest.Name;
            res.Stock = productRequest.Stock;
            res.Price = productRequest.Price;
            res.CategoryId = productRequest.CategoryId;


            _dbContext.Products.Update(res);
            _dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {

            var res = _dbContext.Products.Find(id);

            if (res == null)
            {
                return NotFound();
            }
            //res.Username = model.Username;
            //res.Password = model.Password;
            _dbContext.Products.Remove(res);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
