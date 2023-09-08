using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for months
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MonthController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.MonthMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public MonthController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.MonthMapper(autoMapper);
        }

        // GET: api/Months
        /// <summary>
        /// get all months
        /// </summary>
        /// <returns>list of months</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Month>>> GetMonths()
        {
            var data = await 
                _bll.MonthService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/Months/5
        /// <summary>
        /// get a month by id
        /// </summary>
        /// <param name="id">month id</param>
        /// <returns>month object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Month>> GetMonth(Guid id)
        {
            var month = await _bll.MonthService.FindAsync(id);
            
            if (month == null)
            {
                return NotFound();
            }

            return _mapper.Map(month)!;
        }

        // PUT: api/Months/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a month
        /// </summary>
        /// <param name="id">month id</param>
        /// <param name="month">month object</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonth(Guid id, Public.DTO.v1.Month month)
        {
            if (id != month.Id)
            {
                return BadRequest();
            }
            
            var bllMonth = _mapper.Map(month);

            _bll.MonthService.Update(bllMonth!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Months
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new month to database
        /// </summary>
        /// <param name="month">month object to add</param>
        /// <returns>added month object</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Month>> PostMonth(Public.DTO.v1.Month month)
        {
            var bllMonth = _mapper.Map(month);
            _bll.MonthService.Add(bllMonth!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMonth", new {id = month.Id}, month);

        }

        // DELETE: api/Months/5
        /// <summary>
        /// delete a month
        /// </summary>
        /// <param name="id">month id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonth(Guid id)
        {
            var month = await _bll.MonthService.RemoveAsync(id);

            if (month == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
