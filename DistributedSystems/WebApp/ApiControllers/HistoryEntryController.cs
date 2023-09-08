using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for history entry
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HistoryEntryController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.HistoryEntryMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public HistoryEntryController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.HistoryEntryMapper(autoMapper);
        }

        // GET: api/HistoryEntrys
        /// <summary>
        /// get all history entries
        /// </summary>
        /// <returns>list of history entries</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.HistoryEntry>>> GetHistoryEntrys()
        {
            var data = await 
                _bll.HistoryEntryService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/HistoryEntrys/5
        /// <summary>
        /// get a history entry by id
        /// </summary>
        /// <param name="id">history entry id</param>
        /// <returns>history entry object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.HistoryEntry>> GetHistoryEntry(Guid id)
        {
            var historyEntry = await _bll.HistoryEntryService.FindAsync(id);
            
            if (historyEntry == null)
            {
                return NotFound();
            }

            return _mapper.Map(historyEntry)!;
        }

        // PUT: api/HistoryEntrys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a history entry
        /// </summary>
        /// <param name="id">history entry id</param>
        /// <param name="historyEntry">history entry object</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryEntry(Guid id, Public.DTO.v1.HistoryEntry historyEntry)
        {
            if (id != historyEntry.Id)
            {
                return BadRequest();
            }
            
            var bllHistoryEntry = _mapper.Map(historyEntry);

            _bll.HistoryEntryService.Update(bllHistoryEntry!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/HistoryEntrys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new history entry
        /// </summary>
        /// <param name="historyEntry">history entry object to add</param>
        /// <returns>added history entry object</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.HistoryEntry>> PostHistoryEntry(Public.DTO.v1.HistoryEntry historyEntry)
        {
            var bllHistoryEntry = _mapper.Map(historyEntry);
            _bll.HistoryEntryService.Add(bllHistoryEntry!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetHistoryEntry", new {id = historyEntry.Id}, historyEntry);

        }

        // DELETE: api/HistoryEntrys/5
        /// <summary>
        /// delete a history entry
        /// </summary>
        /// <param name="id">history entry id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryEntry(Guid id)
        {
            var historyEntry = await _bll.HistoryEntryService.RemoveAsync(id);

            if (historyEntry == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
