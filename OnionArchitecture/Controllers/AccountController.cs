using OnionArchitecture.Core.Interfaces.ApplicationServices;
using OnionArchitecture.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace OnionArchitecture.Controllers
{
    public class AccountController : ApiController
    {
        IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IHttpActionResult RegisterAccount(User user)
        {
            string result = "Please Enter Username & Password";
            if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
            {
                result = userService.Register(user);
            }
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            user.IsValid = false;
            if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
            {
                var result = userService.GetUsers(user);
                if(result.Count() > 0)
                {
                    user.Password = null;
                    user.Token = createToken(user.Username);
                    user.IsValid = true;
                    return Json(new { result = user });
                }
                else
                {
                    return Json(new { result = user });
                }
            }
            return Json(new { result = user });
        }

        private string createToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddMinutes(60);

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(
                        subject: claimsIdentity,
                        notBefore: issuedAt,
                        expires: expires,
                        signingCredentials: signingCredentials
                        );
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
