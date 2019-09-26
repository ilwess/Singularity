using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Singularity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Get([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            return Ok();
        }
    }
}