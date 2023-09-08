using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for event type (parent class for reminder type and history event type)
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.EventTypeMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public EventTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.EventTypeMapper(autoMapper);
        }

        // GET: api/EventTypes
        /// <summary>
        /// get all event types
        /// </summary>
        /// <returns>list of event types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.EventType>>> GetEventTypes()
        {
            var data = await 
                _bll.EventTypeService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/EventTypes/5
        /// <summary>
        /// get a event type by id
        /// </summary>
        /// <param name="id">event type id</param>
        /// <returns>event type</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.EventType>> GetEventType(Guid id)
        {
            var reminderType = await _bll.EventTypeService.FindAsync(id);
            
            if (reminderType == null)
            {
                return NotFound();
            }

            return _mapper.Map(reminderType)!;
        }

        // PUT: api/EventTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update event type
        /// </summary>
        /// <param name="id">event type id</param>
        /// <param name="reminderType">event type</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventType(Guid id, Public.DTO.v1.EventType reminderType)
        {
            if (id != reminderType.Id)
            {
                return BadRequest();
            }
            
            var bllEventType = _mapper.Map(reminderType);

            _bll.EventTypeService.Update(bllEventType!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/EventTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new event type
        /// </summary>
        /// <param name="reminderType">event type</param>
        /// <returns>added event type</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.EventType>> PostEventType(Public.DTO.v1.EventType reminderType)
        {
            var bllEventType = _mapper.Map(reminderType);
            _bll.EventTypeService.Add(bllEventType!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetEventType", new {id = reminderType.Id}, reminderType);

        }

        // DELETE: api/EventTypes/5
        /// <summary>
        /// delete a event type
        /// </summary>
        /// <param name="id">event type id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventType(Guid id)
        {
            var reminderType = await _bll.EventTypeService.RemoveAsync(id);

            if (reminderType == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
