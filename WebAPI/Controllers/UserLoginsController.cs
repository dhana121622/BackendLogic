using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]  // Route template for the controller
    [ApiController]   //Marks this as an API controller
    public class UserLoginsController : ControllerBase //controller class that inherits from ControllerBase, which is commonly used for Web APIs.
    {
        private readonly UserLoginDbContext _context; //Declares a read-only _context variable to interact with the database.

        public UserLoginsController(UserLoginDbContext context)  //Receives an instance of UserLoginDbContext, a database context, through dependency injection. This is used to perform database operations.
        {
            _context = context;
        }






        // Create a New User
        [HttpPost]
        public async Task<IActionResult> PostUserLogin(UserLogin userLogin)
        {

            // Check if the ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.UserName == userLogin.UserName);



                if (member == null)
                {

                    string resp;
                    Email_2_UserName_Psd e = new Email_2_UserName_Psd();
                    bool resmessage = e.email_2_UserName_Psd(userLogin.Email, userLogin.Password, userLogin.UserName);

                    if (resmessage)
                    {
                        _context.UserLoginApi.Add(userLogin);
                        await _context.SaveChangesAsync();
                        resp = "New user created successfully, and login credentials have been sent to the registered email ID.";
                    }
                    else
                    {
                        resp = "Some Error Sending Login Credentials to User! please check the Email Id";
                    }
                    return Ok(resp);
                }
                else
                {
                    return Content("USerName Already Exist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user : {ex.Message}");
            }
        }







        //Both Admin & User Login
        [HttpPost("Login")]
        public async Task<IActionResult> VerifyEmailAndPhone([FromBody] VerificationResponse res)
        {

            // Search for the member by email
            var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == res.Email);

            var member1 = await _context.AdminLogin.FirstOrDefaultAsync(m => m.Email == res.Email);


            // Check if the member exists
            if (member != null)
            {
                bool isMatch = member.Password == res.Password;

                if (isMatch)
                {
                    return Ok("User");
                }
                return NotFound("Email and Password do not match.");
            }
            else if (member1 != null)
            {
                bool isMatch = member1.Password == res.Password;

                if (isMatch)
                {
                    return Ok("Admin");
                }
                return NotFound("Email and Password do not match.");
            }
            else
            {
                return NotFound("Email not found");
            }
        }


        //If User Forget their Password Means this method send a otp to reset the password
        [HttpPost("EmailCheck")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerificationResponse res)
        {
            // Check if the ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Search for the member by email
            var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == res.Email);
            var email = res.Email;

            //// Check if the member exists
            if (member == null)
            {
                return NotFound("Email not found");
            }
            else
            {
                EmailAuthProcess em = new EmailAuthProcess();

                string response = em.emailAuthProcess(email);

                string resp = "Failed";

                bool areEqual = response.Equals(resp);

                if (!areEqual)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound("Failed");
                }
            }
        }


        //Update the new Password in Database
        [HttpPost("UpdatePsd")]
        public async Task<IActionResult> PutMember([FromBody] PasswordUpdate member)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Search for the member by email
            var member1 = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == member.Email);

            // Check if the member exists
            if (member1 == null)
            {
                return NotFound("Email not found");
            }
            else
            {
                member1.Password = member.Password;
                member1.ConfirmPassword = member.ConfirmPassword;
                _context.Entry(member1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Password Updated");
            }
        }


        [HttpPost("RetriveUserName")]
        public async Task<IActionResult> RetriveUserName([FromBody] VerificationResponse res)
        {
            var message = "Email not found";

            // Search for the member by email
            var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == res.Email);

            var member1 = await _context.AdminLogin.FirstOrDefaultAsync(m => m.Email == res.Email);

            // Check if the member exists
            if (member != null)
            {
                message = member.UserName;
            }
            else if (member1 != null)
            {
                message = member1.UserName;
            }


            return Ok(message);

        }


        [HttpPost("RetriveUserName1")]
        public async Task<IActionResult> RetriveUserName1([FromBody] VerificationResponse res)
        {
            // Check if the ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Search for the member by email
            var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == res.Email);


            var username = member.UserName;

            // Check if the member exists
            if (member == null)
            {
                return NotFound("Email not found");
            }
            else
            {
                return Ok(username);
            }

        }


        //Retrive the UserInformation through Email
        [HttpPost("User_Information")]
        public async Task<IActionResult> User_Information([FromBody] String Email)
        {
            try
            {
                var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == Email);

                return Ok(member);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
        }


        //Save the Profile Photo In DB
        [HttpPost("profile_photo")]
        public async Task<IActionResult> ProfilePhoto([FromBody] VerificationResponse res)
        {
            var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == res.Email);
           
            try
            {    
                member.Profile_Image = res.Profile_Image;

                // Save changes to the database                                 
                await _context.SaveChangesAsync();
                return Ok("Profile photo updated successfully" );
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Profile_Image format");
            }                                                                            
        }






        [HttpPost("checkDbForGroups")]
        public async Task<IActionResult> CheckDbForGroups([FromBody] dummy d)
        {
            
            var result = await _context.GroupDetails
                                       .Where(g => g.UserName.Contains(d.UserName))
                                       .Select(g => new
                                       {
                                           g.GroupName,
                                           g.Description
                                       })
                                       .ToListAsync();

            return Ok(result);
        }

    }
}         
