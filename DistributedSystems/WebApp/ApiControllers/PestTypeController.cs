using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for pest type
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PestTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PestTypeMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PestTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PestTypeMapper(autoMapper);
        }

        // GET: api/PestTypes
        /// <summary>
        /// get all pest types
        /// </summary>
        /// <returns>list of pest types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PestType>>> GetPestTypes()
        {
            var data = await 
                _bll.PestTypeService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/PestTypes/5
        /// <summary>
        /// get a pest type by id
        /// </summary>
        /// <param name="id">pest type id</param>
        /// <returns>pest type</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.PestType>> GetPestType(Guid id)
        {
            var pestType = await _bll.PestTypeService.FindAsync(id);
            
            if (pestType == null)
            {
                return NotFound();
            }

            return _mapper.Map(pestType)!;
        }

        // PUT: api/PestTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a pest type
        /// </summary>
        /// <param name="id">pest type id</param>
        /// <param name="pestType">pest type object</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPestType(Guid id, Public.DTO.v1.PestType pestType)
        {
            if (id != pestType.Id)
            {
                return BadRequest();
            }
            
            var bllPestType = _mapper.Map(pestType);

            _bll.PestTypeService.Update(bllPestType!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/PestTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new pest type
        /// </summary>
        /// <param name="pestType">pest type object</param>
        /// <returns>added pest type</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.PestType>> PostPestType(Public.DTO.v1.PestType pestType)
        {
            var bllPestType = _mapper.Map(pestType);
            _bll.PestTypeService.Add(bllPestType!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPestType", new {id = pestType.Id}, pestType);

        }

        // DELETE: api/PestTypes/5
        /// <summary>
        /// delete a pest type
        /// </summary>
        /// <param name="id">pest type id</param>
        /// <returns>noContent</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePestType(Guid id)
        {
            var pestType = await _bll.PestTypeService.RemoveAsync(id);

            if (pestType == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
