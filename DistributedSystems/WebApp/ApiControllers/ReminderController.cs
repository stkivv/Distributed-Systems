using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Helpers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// reminder controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReminderController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.ReminderMapper _mapper;
        private readonly Public.DTO.Mappers.ReminderActiveMonthMapper _activeMonthMapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public ReminderController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.ReminderMapper(autoMapper);
            _activeMonthMapper = new Public.DTO.Mappers.ReminderActiveMonthMapper(autoMapper);
        }

        // GET: api/Reminders
        /// <summary>
        /// get all reminders
        /// </summary>
        /// <returns>list of reminders</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Reminder>>> GetReminders()
        {
            var data = await 
                _bll.ReminderService.AllAsync(User.GetUserId());

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/Reminders/5
        /// <summary>
        /// get reminder by id
        /// </summary>
        /// <param name="id">reminder id</param>
        /// <returns>reminder</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Reminder>> GetReminder(Guid id)
        {
            var reminder = await _bll.ReminderService.FindAsync(id, User.GetUserId());
            
            if (reminder == null)
            {
                return NotFound();
            }

            return _mapper.Map(reminder)!;
        }

        // PUT: api/Reminders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a reminder
        /// </summary>
        /// <param name="id">reminder id</param>
        /// <param name="reminder">reminder</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminder(Guid id, Public.DTO.v1.Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return BadRequest();
            }
            
            var bllReminder = _mapper.Map(reminder);
            bllReminder!.AppUserId = User.GetUserId();
            _bll.ReminderService.Update(bllReminder);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Reminders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new reminder
        /// </summary>
        /// <param name="reminder">reminder</param>
        /// <returns>added reminder</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Reminder>> PostReminder(Public.DTO.v1.Reminder reminder)
        {
            var bllReminder = _mapper.Map(reminder);
            bllReminder!.AppUserId = User.GetUserId();
            var addedReminder = _bll.ReminderService.Add(bllReminder);

            foreach (var month in reminder.Months!)
            {
                var activeMonth = new ReminderActiveMonth()
                {
                    MonthId = month.Id,
                    ReminderId = addedReminder.Id
                };
                _bll.ReminderActiveMonthService.Add(_activeMonthMapper.Map(activeMonth)!);
            }
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReminder", new {id = reminder.Id}, reminder);
        }

        // DELETE: api/Reminders/5
        /// <summary>
        /// delete reminder
        /// </summary>
        /// <param name="id">reminder id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            var reminder = await _bll.ReminderService.RemoveAsync(id, User.GetUserId());

            if (reminder == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
