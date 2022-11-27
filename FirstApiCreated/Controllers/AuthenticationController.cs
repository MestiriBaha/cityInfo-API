using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiCreated.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
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
        [HttpPost("authenticate")]
        public ActionResult<String> authenticate(AuthenticationRequestBody authentication)
        {
            // validate the username/paasword  
            var usercredentials = ValidateUsercredentials(authentication.UserName, authentication.Password);
            if (usercredentials == null)
            {
                return Unauthorized();  
            }

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
