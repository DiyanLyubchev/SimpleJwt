using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.JWT.Token.Interfaces;
using Sample.JWT.Token.Models;
using System.Collections.Generic;


namespace Sample.JWT.Token.Controllers;

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

    /// <summary>
    /// Header = > (key) - Authorization (value) - "Bearer " + token 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Sofia", "Varna" };
    }

    // GET api/<NameController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    /// <summary>
    /// "test1" "password1"
    /// </summary>
    /// <param name="userCred"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("authenticate")]
    //https://www.youtube.com/watch?v=vWkPdurauaA&ab_channel=DotNetCoreCentral
    public IActionResult Authenticate([FromBody] UserCred userCred)
    {
        var token = this.jwtAuthenticationManager
            .Authenticate(userCred.Username, userCred.Password);

        if (token == null)
            return Unauthorized();

        return Ok($"Bearer {token}");
    }
}
