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
using StockCore.Interfaces;
using StockCore.Services;

namespace StockCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        //private readonly DatabaseContext _dbContext;
        //public ProductController(DatabaseContext databaseContext) => this._dbContext = databaseContext;
        public ProductController(IProductService productService) => this.productService = productService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductALL()
        {
            //var res = _dbContext.Products.ToList();
            //return Ok(res);


            ////join with model and select only mapping data
            //return _dbContext.Products.Include(p => p.Category)
            //    .OrderByDescending(p => p.ProductId)
            //    .Select(ProductResponse.FromProduct).ToList();

            return (await productService.findAll())
                .Select(ProductResponse.FromProduct).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            //var res = _dbContext.Products.Find(id);
            //return Ok(res);

            //var res = _dbContext.Products.Include(p => p.Category)
            //    .SingleOrDefault(p => p.ProductId == id);
            ////query on ly one data , if null then return null

            //if (res == null)
            //{
            //    return NotFound();
            //}
            //return ProductResponse.FromProduct(res);

            var product = await productService.FindById(id);
            //query on ly one data , if null then return null

            if (product == null)
            {
                return NotFound();
            }
            //return new productService({
            //    ProductId = ProductResponse.ProductId,
            //    Name = ProductResponse.Name,
            //    Price = ProductResponse.Price,
            //    Image = ProductResponse.Image,
            //    Stock = ProductResponse.Stock,
            //    CategoryName = ProductResponse.Category.Name,
            //})
            return ProductResponse.FromProduct(product);



        }
        [HttpGet("search")]

        public async Task<ActionResult<IEnumerable<ProductResponse>>> SearchProduct([FromQuery] string name = "")
        {
            //var res = _dbContext.Products.Include(p => p.Category)
            //    .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            //    .Select(ProductResponse.FromProduct).ToList();
            //return res;

            var res = (await productService.Search(name))
             .Select(ProductResponse.FromProduct).ToList();
            return res;

        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProductBy([FromForm] ProductRequest productRequest)
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

                ////var res = ProductRequest.Adapt(Product);


                //_dbContext.Products.Add(product);
                //_dbContext.SaveChanges();
                ////return StatusCode(201);
                ////return Ok();
                //return StatusCode((int)HttpStatusCode.Created);

                await productService.Create(product);
                //productService.SaveChang();
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
        public async Task<ActionResult<Product>> UpdateAccount(int id, [FromForm] ProductRequest productRequest)
        {
            //if (id != productRequest.ProductId)
            //{
            //    return BadRequest();
            //}
            //var res = _dbContext.Products.Find(id);

            //if (res == null)
            //{
            //    return NotFound();
            //}
            ////res.Name = productRequest.Name;
            ////res.Price = productRequest.Price;
            ////res.Stock = productRequest.Stock;
            //res.ProductId = productRequest.ProductId;
            //res.Name = productRequest.Name;
            //res.Stock = productRequest.Stock;
            //res.Price = productRequest.Price;
            //res.CategoryId = productRequest.CategoryId;


            //_dbContext.Products.Update(res);
            //_dbContext.SaveChanges();
            //return NoContent();

            if (id != productRequest.ProductId)
            {
                return BadRequest();
            }
            var res = await productService.FindById(id);

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


            await productService.Update(id,res);
            //_dbContext.SaveChanges();
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {

            //var res = _dbContext.Products.Find(id);

            //if (res == null)
            //{
            //    return NotFound();
            //}
            ////res.Username = model.Username;
            ////res.Password = model.Password;
            //_dbContext.Products.Remove(res);
            //_dbContext.SaveChanges();
            //return NoContent();

            var res = await productService.FindById(id);

            if (res == null)
            {
                return NotFound();
            }
            //res.Username = model.Username;
            //res.Password = model.Password;
            await productService.Delete(res);
            //await productService.SaveChanges();
            return NoContent();
        }
    }
}
