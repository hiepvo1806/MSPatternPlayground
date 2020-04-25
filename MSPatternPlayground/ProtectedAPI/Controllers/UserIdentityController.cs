using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProtectedAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserIdentityController : ControllerBase
    {
        // GET: api/UserIdentity
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value 1", "value 2" };
        }
    }
}
