using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Blog.Core.IServices;
using Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.AuthHelper.OverWrite;

namespace Blog.Core.Controllers
{
    [Route("api/Blog")]
    [Authorize]
    [Produces("application/json")]
    public class BlogController : Controller
    {
        /// <summary>
        /// sum
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpGet]
        public int Get(int i,int j)
        {
            IAdvertisementServices advertisementServices = new AdvertisementServices();
            return advertisementServices.Sum(i, j);
        }

        // GET: api/Blog/5
        [HttpGet("GetToken")]
        [AllowAnonymous]
        public string GetToken()
        {
            TokenModelJwt tokenModel = new TokenModelJwt
            {
                Role="Admin",
                Uid=1,
                Work=""
            };
            return JwtHelper.IssueJwt(tokenModel); ;
        }

        // POST: api/Blog
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Blog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}