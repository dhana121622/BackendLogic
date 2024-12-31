using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkUpdateController : ControllerBase
    {
        private readonly UserLoginDbContext _context;


        public WorkUpdateController(UserLoginDbContext context)
        {
            _context = context;
        }


        [HttpPost("WorkUpdate")]
        public async Task<IActionResult> WorkUpdate(WorkUpdate workUpdate)
        {

            // Check if the ModelState is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.WorkUpdate.Add(workUpdate);
                await _context.SaveChangesAsync();
                return Ok("Successfully Updated.");
                //return Redirect("DisplayWorkUpdate");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user : {ex.Message}");
            }
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<WorkUpdate>>> GetMembersByEmail(string email)
        {
            var members = await _context.WorkUpdate
                                        .Where(w => w.Email == email)
                                        .ToListAsync();

            if (members == null || !members.Any())
            {
                return NotFound();
            }

            return Ok(members);
        }


        [HttpPost("Delete")]
        public IActionResult Delete(WorkUpdate wrk)
        {
            if (wrk == null)
            {
                return BadRequest("Id is Null");
            }

            var member = _context.WorkUpdate.FirstOrDefault(x => x.Id == wrk.Id);

            _context.WorkUpdate.Remove(member);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost("ExistingDetailsUpdate")]
        public IActionResult ExistingDetailsUpdate(WorkUpdate wrk)
        {
            if (wrk == null)
            {
                return BadRequest("Id is Null");
            }

            //var member = _context.WorkUpdate.FirstOrDefault(x => x.Id == wrk.Id);

            _context.WorkUpdate.Update(wrk);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost("AdminValidateResponse")]
        public IActionResult AdminValidateResponse(AdminValidateRes wrk)
        {
            var member = _context.WorkUpdate.FirstOrDefault(x => x.Id == wrk.Id);

            var statusmessage = member.feedbackmessage;
            return Ok(statusmessage);
        }

    }
}