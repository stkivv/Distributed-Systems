using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// reminder active month controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReminderActiveMonthController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.ReminderActiveMonthMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public ReminderActiveMonthController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.ReminderActiveMonthMapper(autoMapper);
        }

        // GET: api/ReminderActiveMonths
        /// <summary>
        /// get all reminder active months
        /// </summary>
        /// <returns>list of reminder active months</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.ReminderActiveMonth>>> GetReminderActiveMonths()
        {
            var data = await 
                _bll.ReminderActiveMonthService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/ReminderActiveMonths/5
        /// <summary>
        /// get a reminder active month by id
        /// </summary>
        /// <param name="id">reminder active month id</param>
        /// <returns>reminder active month</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.ReminderActiveMonth>> GetReminderActiveMonth(Guid id)
        {
            var reminderActiveMonth = await _bll.ReminderActiveMonthService.FindAsync(id);
            
            if (reminderActiveMonth == null)
            {
                return NotFound();
            }

            return _mapper.Map(reminderActiveMonth)!;
        }

        // PUT: api/ReminderActiveMonths/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update reminder active month
        /// </summary>
        /// <param name="id">reminder active month id</param>
        /// <param name="reminderActiveMonth">reminder active months</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminderActiveMonth(Guid id, Public.DTO.v1.ReminderActiveMonth reminderActiveMonth)
        {
            if (id != reminderActiveMonth.Id)
            {
                return BadRequest();
            }
            
            var bllReminderActiveMonth = _mapper.Map(reminderActiveMonth);

            _bll.ReminderActiveMonthService.Update(bllReminderActiveMonth!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/ReminderActiveMonths
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new reminder active month
        /// </summary>
        /// <param name="reminderActiveMonth">reminder active months</param>
        /// <returns>added reminder active month</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.ReminderActiveMonth>> PostReminderActiveMonth(Public.DTO.v1.ReminderActiveMonth reminderActiveMonth)
        {
            var bllReminderActiveMonth = _mapper.Map(reminderActiveMonth);
            _bll.ReminderActiveMonthService.Add(bllReminderActiveMonth!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReminderActiveMonth", new {id = reminderActiveMonth.Id}, reminderActiveMonth);

        }

        // DELETE: api/ReminderActiveMonths/5
        /// <summary>
        /// delete reminder active month
        /// </summary>
        /// <param name="id">reminder active month id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminderActiveMonth(Guid id)
        {
            var reminderActiveMonth = await _bll.ReminderActiveMonthService.RemoveAsync(id);

            if (reminderActiveMonth == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
