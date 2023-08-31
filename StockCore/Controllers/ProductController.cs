using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockCore.Data;
using StockCore.Entities;
using System.Net;

namespace StockCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        public ProductController(DatabaseContext databaseContext) => this._dbContext = databaseContext;

        [HttpGet]
        public ActionResult<Product> GetProductALL()
        {
           var res = _dbContext.Products.ToList();
           return Ok(res);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var res = _dbContext.Products.Find(id);
            return Ok(res);
        }
        [HttpPost]
        public ActionResult<Product> AddProductBy([FromForm] Product model)
        {
            try
            {
                _dbContext.Products.Add(model);
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
        public ActionResult<Product> UpdateAccount(int id, [FromForm] Product model)
        {
            if (id != model.ProductId)
            {
                return BadRequest();
            }
            var res = _dbContext.Products.Find(id);

            if (res == null)
            {
                return NotFound();
            }
            res.Name = model.Name;
            res.Price = model.Price;
            res.Stock = model.Stock;
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
