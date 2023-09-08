using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// plant tag controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlantTagController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PlantTagMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PlantTagController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PlantTagMapper(autoMapper);
        }

        // GET: api/PlantTags
        /// <summary>
        /// get all plant tags
        /// </summary>
        /// <returns>list of plant tags</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PlantTag>>> GetPlantTags()
        {
            var data = await 
                _bll.PlantTagService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/PlantTags/5
        /// <summary>
        /// get a plant tag by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>plant tag</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.PlantTag>> GetPlantTag(Guid id)
        {
            var plantTag = await _bll.PlantTagService.FindAsync(id);
            
            if (plantTag == null)
            {
                return NotFound();
            }

            return _mapper.Map(plantTag)!;
        }

        // PUT: api/PlantTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a plant tag
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="plantTag">plant tag</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantTag(Guid id, Public.DTO.v1.PlantTag plantTag)
        {
            if (id != plantTag.Id)
            {
                return BadRequest();
            }
            
            var bllPlantTag = _mapper.Map(plantTag);

            _bll.PlantTagService.Update(bllPlantTag!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/PlantTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new plant tag
        /// </summary>
        /// <param name="plantTag">plant tag</param>
        /// <returns>added plant tag</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.PlantTag>> PostPlantTag(Public.DTO.v1.PlantTag plantTag)
        {
            var bllPlantTag = _mapper.Map(plantTag);
            _bll.PlantTagService.Add(bllPlantTag!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPlantTag", new {id = plantTag.Id}, plantTag);

        }

        // DELETE: api/PlantTags/5
        /// <summary>
        /// delete plant tag
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantTag(Guid id)
        {
            var plantTag = await _bll.PlantTagService.RemoveAsync(id);

            if (plantTag == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
