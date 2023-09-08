using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for history entry type
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HistoryEntryTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.HistoryEntryTypeMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public HistoryEntryTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.HistoryEntryTypeMapper(autoMapper);
        }

        // GET: api/HistoryEntryTypes
        /// <summary>
        /// get all history entry types
        /// </summary>
        /// <returns>list of history entry types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.HistoryEntryType>>> GetHistoryEntryTypes()
        {
            var data = await 
                _bll.HistoryEntryTypeService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/HistoryEntryTypes/5
        /// <summary>
        /// get a history entry type by id
        /// </summary>
        /// <param name="id">history entry type id</param>
        /// <returns>history entry type object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.HistoryEntryType>> GetHistoryEntryType(Guid id)
        {
            var historyEntryType = await _bll.HistoryEntryTypeService.FindAsync(id);
            
            if (historyEntryType == null)
            {
                return NotFound();
            }

            return _mapper.Map(historyEntryType)!;
        }

        // PUT: api/HistoryEntryTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a history entry type
        /// </summary>
        /// <param name="id">history entry type id</param>
        /// <param name="historyEntryType">history entry type object</param>
        /// <returns>noContent</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryEntryType(Guid id, Public.DTO.v1.HistoryEntryType historyEntryType)
        {
            if (id != historyEntryType.Id)
            {
                return BadRequest();
            }
            
            var bllHistoryEntryType = _mapper.Map(historyEntryType);

            _bll.HistoryEntryTypeService.Update(bllHistoryEntryType!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/HistoryEntryTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new history entry type
        /// </summary>
        /// <param name="historyEntryType">history entry type to add</param>
        /// <returns>added history entry type</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.HistoryEntryType>> PostHistoryEntryType(Public.DTO.v1.HistoryEntryType historyEntryType)
        {
            var bllHistoryEntryType = _mapper.Map(historyEntryType);
            _bll.HistoryEntryTypeService.Add(bllHistoryEntryType!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetHistoryEntryType", new {id = historyEntryType.Id}, historyEntryType);

        }

        // DELETE: api/HistoryEntryTypes/5
        /// <summary>
        /// delete a history entry type
        /// </summary>
        /// <param name="id">history entry type id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryEntryType(Guid id)
        {
            var historyEntryType = await _bll.HistoryEntryTypeService.RemoveAsync(id);

            if (historyEntryType == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
