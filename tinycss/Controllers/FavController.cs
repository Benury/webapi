using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCSS_Webapi.Repository;
using TinyCSS_Webapi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TinyCSS_Webapi.Controllers
{
    [Route("api/[controller]")]
    public class FavController : Controller
    {
        // GET: api/Fav
        [HttpGet]
        public List<tblfav> Get()
        {
            return FavRepository.Get();
        }

        // GET api/Fav/userid
        [HttpGet("{userid}")]
        public List<tblfav> Get(string userid)
        {
            return FavRepository.GetFavByUserid(userid);
        }
        // GET api/Fav/userid
        [HttpGet("{userid}/{type}")]
        public List<tblfav> Get(string userid,string type)
        {
            return FavRepository.GetFavByUseridAndType(userid,type);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm]tblfav fav)
        {
            FavRepository.AddFav(fav);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpPost("delete")]
        public void DeleteFav(int fguid)
        {
            FavRepository.DelFav(fguid);
        }
    }
}
