using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.JWT.Token.Interfaces;
using Sample.JWT.Token.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.JWT.Token.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public NameController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/<NameController>
        // Header = > (key) - Authorization (value) - "Bearer " + token 
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string authHeader = Request.Headers["Authorization"];
            return new string[] { "Sofia", "Varna" };
        }

        // GET api/<NameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        //https://www.youtube.com/watch?v=vWkPdurauaA&ab_channel=DotNetCoreCentral
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = this.jwtAuthenticationManager
                .Authenticate(userCred.Username, userCred.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new TokenModel { JwtToken = token });
        }
    }
}
