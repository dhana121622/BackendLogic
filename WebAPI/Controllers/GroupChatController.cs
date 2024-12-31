using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        private readonly UserLoginDbContext _context;
        public GroupChatController(UserLoginDbContext context)
        {
            _context = context;
        }


        [HttpPost("GroupChat")]
        public async Task<IActionResult> GroupChat(Group_Chat message)
        {
            try
            {
                _context.GroupChat.Add(message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user : {ex.Message}");
            }
        }


        [HttpPost("RetriveChatMessage")]
        public async Task<ActionResult<IEnumerable<WorkUpdate>>> RetriveChatMessage(Group_Chat message)
        {
            var members = await _context.GroupChat
                                        .Where(w => w.GroupName == message.GroupName)
                                        .ToListAsync();

            //if (members == null || !members.Any())
            //{
            //    return NotFound();
            //}

            return Ok(members);
        }


        [HttpPost("RetriveUserName")]
        public async Task<IActionResult> RetriveUserName([FromBody] VerificationResponse res)
        {
            // Check if the ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Search for the member by email
            var member = await _context.UserLoginApi.FirstOrDefaultAsync(m => m.Email == res.Email);

            var member1 = await _context.AdminLogin.FirstOrDefaultAsync(m => m.Email == res.Email);

            string username = "";

            // Check if the member exists
            if (member != null)
            {
                username = member.UserName;
            }
            else if (member1 != null)
            {
                username = member1.UserName;
            }
            return Ok(username);

        }


        [HttpPost("NewCreate")]
        public async Task<IActionResult> NewCreate(Group_Chat message)
        {
            try
            {
                _context.GroupChat.Add(message);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user : {ex.Message}");
            }
        }


        [HttpGet("Retrive_the_Group_Name")]
        public async Task<ActionResult> Retrive_the_Group_Name()
        {
            try
            {

                var groupNames = await _context.GroupChat.Select(g => g.GroupName).ToListAsync();



                return Ok(groupNames);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user : {ex.Message}");
            }
        }
    }
}