﻿using Microsoft.IdentityModel.Tokens;
using Sample.JWT.Token.Common;
using Sample.JWT.Token.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Sample.JWT.Token.Managers;

public class JwtAuthenticationManager : IJwtAuthenticationManager
{
    //Read users from db 
    private readonly IDictionary<string, string> users = new Dictionary<string, string>
    {
            { "test1", "password1" },
            { "test2", "password2" }
    };

    private readonly string key;
    public JwtAuthenticationManager(string key)
    {
        this.key = key;
    }

    public string Authenticate(string username, string password)
    {
        if (!users.Any(u => u.Key == username && u.Value == password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(key);
        var tockenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, username)// subject authenticate
            }),
            Expires = DateTime.UtcNow.AddHours(JwtContainerModel.ExpireHoures), //Expiration time tocken
            SigningCredentials = new SigningCredentials //provide signing credentials and how sign (with algorithms HmacSha256Signature)
            (
                new SymmetricSecurityKey(tokenKey),
                JwtContainerModel.SecurityAlgorithm
            )
        };

        var token = tokenHandler.CreateToken(tockenDescriptor);//get tocken

        return tokenHandler.WriteToken(token); //string jwt tocken 
    }
}
