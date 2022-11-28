using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApiCreated.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // highly sensitive class !! for security measures !! 
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }    
            public string? Password { get; set; }
        }
            private class CityInfoUser
        {
            public int  UserId { get; set; }  
            public string  UserName { get; set; }
            public string FirstName { get; set; }  
            public string LastName { get; set; }
            public string City { get; set; }   

            public CityInfoUser(int userId, string userName, string firstName, string lastName, string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }
        
        public AuthenticationController (IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        [HttpPost("authenticate")]
       public ActionResult<String> authenticate(AuthenticationRequestBody authentication)
        {
            // validate the username/paasword  
            var user = ValidateUsercredentials(authentication.UserName, authentication.Password);
            if (user == null)
            {
                return Unauthorized();  
            }
            // create the Token  : always pay attention for the configuration string !! 
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"] ) ) ;
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256) ;
            // the claims that : 
            var claimsforToken = new List<Claim>();
                claimsforToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsforToken.Add(new Claim("given_name ", user.FirstName));
            claimsforToken.Add(new Claim("family_name", user.LastName));
            claimsforToken.Add(new Claim("city ", user.City));
            //finally : creation of the Token 
            var JwtsecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsforToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );
            // write the Token 
            var TokentoReturn = new JwtSecurityTokenHandler().WriteToken(JwtsecurityToken);

            return Ok(TokentoReturn);   



        }

        private CityInfoUser ValidateUsercredentials(string? userName, string? password)
        {
            // we should have used a table or Db , if we have we just check the user from the DB data 
            return new CityInfoUser (
                1,
                userName ?? "bouha",
                "baha",
                "mestiri",
                "Tunis");    }
    }
}
