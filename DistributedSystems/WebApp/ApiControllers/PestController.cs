using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for pests
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PestController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PestMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PestController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PestMapper(autoMapper);
        }

        // GET: api/Pests
        /// <summary>
        /// get all pests
        /// </summary>
        /// <returns>list of pests</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Pest>>> GetPests()
        {
            var data = await 
                _bll.PestService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/Pests/5
        /// <summary>
        /// get a pest by id
        /// </summary>
        /// <param name="id">pest id</param>
        /// <returns>pest object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Pest>> GetPest(Guid id)
        {
            var pest = await _bll.PestService.FindAsync(id);
            
            if (pest == null)
            {
                return NotFound();
            }

            return _mapper.Map(pest)!;
        }

        // PUT: api/Pests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a pest
        /// </summary>
        /// <param name="id">pest id</param>
        /// <param name="pest">pest object</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPest(Guid id, Public.DTO.v1.Pest pest)
        {
            if (id != pest.Id)
            {
                return BadRequest();
            }
            
            var bllPest = _mapper.Map(pest);

            _bll.PestService.Update(bllPest!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Pests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add new pest
        /// </summary>
        /// <param name="pest">pest object</param>
        /// <returns>added pest object</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Pest>> PostPest(Public.DTO.v1.Pest pest)
        {
            var bllPest = _mapper.Map(pest);
            _bll.PestService.Add(bllPest!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPest", new {id = pest.Id}, pest);

        }

        // DELETE: api/Pests/5
        /// <summary>
        /// delete a pest
        /// </summary>
        /// <param name="id">pest id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePest(Guid id)
        {
            var pest = await _bll.PestService.RemoveAsync(id);

            if (pest == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
