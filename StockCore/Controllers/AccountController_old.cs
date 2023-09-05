using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockCore.Data;
using StockCore.Entities;
using System.Net;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController_old : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public AccountController_old(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccount()
        {
            //var accountvalues = _databaseContext.Accounts.OrderByDescending( c => c.AccountId).ToList();
            var result = _databaseContext.Accounts.OrderBy(c => c.AccountId).ToList();

            //return _databaseContext.Accounts.OrderbyDescending(p => p.)ToList();
            return result;
            //return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public ActionResult<Account> GetByid(int id)
        {
            //var accountvalues = _databaseContext.Accounts.Where(v => v.RoleId == id).ToList();
            var result = _databaseContext.Accounts.Find(id);
            if(result==null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<Account>> Search([FromQuery] string name="")
        {
            //var accountvalues = _databaseContext.Accounts.Where(v => v.RoleId == id).ToList();
            var result = _databaseContext.Accounts
                .Where(a => a.Username.ToLower().Contains(name.ToLower()))
      
                .ToList();
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        // POST api/<AccountController>
        [HttpPost]
        public ActionResult<Account> AddAccount([FromForm] Account model)
        {
            try
            {
                _databaseContext.Accounts.Add(model);
                _databaseContext.SaveChanges();
                //return StatusCode(201);
                //return Ok();
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                throw (e);
            }
            
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public ActionResult<Account> UpdateAccount(int id, [FromForm] Account model)
        {
            if(id != model.AccountId)
            {
                return BadRequest();
            }
            var res = _databaseContext.Accounts.Find(id);

            if (res == null)
            {
                return NotFound();
            }
            res.Username = model.Username;
            res.Password = model.Password;
            _databaseContext.Accounts.Update(res);
            _databaseContext.SaveChanges();
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
   
            var res = _databaseContext.Accounts.Find(id);

            if (res == null)
            {
                return NotFound();
            }
            //res.Username = model.Username;
            //res.Password = model.Password;
            _databaseContext.Accounts.Remove(res);
            _databaseContext.SaveChanges();
            return NoContent();
        }
    }

   
}
