using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for reminder type
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReminderTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.ReminderTypeMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public ReminderTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.ReminderTypeMapper(autoMapper);
        }

        // GET: api/ReminderTypes
        /// <summary>
        /// get all reminder types
        /// </summary>
        /// <returns>list of reminder types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.ReminderType>>> GetReminderTypes()
        {
            var data = await 
                _bll.ReminderTypeService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/ReminderTypes/5
        /// <summary>
        /// get a reminder type by id
        /// </summary>
        /// <param name="id">reminder type id</param>
        /// <returns>reminder type</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.ReminderType>> GetReminderType(Guid id)
        {
            var reminderType = await _bll.ReminderTypeService.FindAsync(id);
            
            if (reminderType == null)
            {
                return NotFound();
            }

            return _mapper.Map(reminderType)!;
        }

        // PUT: api/ReminderTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update reminder type
        /// </summary>
        /// <param name="id">reminder type id</param>
        /// <param name="reminderType">reminder type</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminderType(Guid id, Public.DTO.v1.ReminderType reminderType)
        {
            if (id != reminderType.Id)
            {
                return BadRequest();
            }
            
            var bllReminderType = _mapper.Map(reminderType);

            _bll.ReminderTypeService.Update(bllReminderType!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/ReminderTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new reminder type
        /// </summary>
        /// <param name="reminderType">reminder type</param>
        /// <returns>added reminder type</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.ReminderType>> PostReminderType(Public.DTO.v1.ReminderType reminderType)
        {
            var bllReminderType = _mapper.Map(reminderType);
            _bll.ReminderTypeService.Add(bllReminderType!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReminderType", new {id = reminderType.Id}, reminderType);

        }

        // DELETE: api/ReminderTypes/5
        /// <summary>
        /// delete a reminder type
        /// </summary>
        /// <param name="id">reminder type id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminderType(Guid id)
        {
            var reminderType = await _bll.ReminderTypeService.RemoveAsync(id);

            if (reminderType == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
