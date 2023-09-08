using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// pest severity controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PestSeverityController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PestSeverityMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PestSeverityController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PestSeverityMapper(autoMapper);
        }

        // GET: api/PestSeveritys
        /// <summary>
        /// get all pest severity objects
        /// </summary>
        /// <returns>list of pest severity objects</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PestSeverity>>> GetPestSeveritys()
        {
            var data = await 
                _bll.PestSeverityService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/PestSeveritys/5
        /// <summary>
        /// get pest severity by id
        /// </summary>
        /// <param name="id">pest severity id</param>
        /// <returns>pest severity object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.PestSeverity>> GetPestSeverity(Guid id)
        {
            var pestSeverity = await _bll.PestSeverityService.FindAsync(id);
            
            if (pestSeverity == null)
            {
                return NotFound();
            }

            return _mapper.Map(pestSeverity)!;
        }

        // PUT: api/PestSeveritys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update pest severity
        /// </summary>
        /// <param name="id">pest severity id</param>
        /// <param name="pestSeverity">pest severity object</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPestSeverity(Guid id, Public.DTO.v1.PestSeverity pestSeverity)
        {
            if (id != pestSeverity.Id)
            {
                return BadRequest();
            }
            
            var bllPestSeverity = _mapper.Map(pestSeverity);

            _bll.PestSeverityService.Update(bllPestSeverity!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/PestSeveritys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add new pest severity
        /// </summary>
        /// <param name="pestSeverity">pest severity object</param>
        /// <returns>added pest severity</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.PestSeverity>> PostPestSeverity(Public.DTO.v1.PestSeverity pestSeverity)
        {
            var bllPestSeverity = _mapper.Map(pestSeverity);
            _bll.PestSeverityService.Add(bllPestSeverity!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPestSeverity", new {id = pestSeverity.Id}, pestSeverity);

        }

        // DELETE: api/PestSeveritys/5
        /// <summary>
        /// delete a pest severity
        /// </summary>
        /// <param name="id">pest severity id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePestSeverity(Guid id)
        {
            var pestSeverity = await _bll.PestSeverityService.RemoveAsync(id);

            if (pestSeverity == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
