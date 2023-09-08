using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Helpers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// plant controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlantController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PlantMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="autoMapper"></param>
        public PlantController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PlantMapper(autoMapper);
        }

        // GET: api/Plants
        /// <summary>
        /// get all plants
        /// </summary>
        /// <returns>list of plants</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Plant>>> GetPlants()
        {
            var data = await 
                _bll.PlantService.AllAsync(User.GetUserId());

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/Plants/5
        /// <summary>
        /// get a plant by id
        /// </summary>
        /// <param name="id">plant id</param>
        /// <returns>plant</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Plant>> GetPlant(Guid id)
        {
            var plant = await _bll.PlantService.FindAsync(id, User.GetUserId());
            
            if (plant == null)
            {
                return NotFound();
            }

            return _mapper.Map(plant)!;
        }

        // PUT: api/Plants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update plant
        /// </summary>
        /// <param name="id">plant id</param>
        /// <param name="plant">plant</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Plant>> PutPlant(Guid id, Public.DTO.v1.Plant plant)
        {
            if (id != plant.Id)
            {
                return BadRequest();
            }
            
            var bllPlant = _mapper.Map(plant);
            bllPlant!.AppUserId = User.GetUserId();
            _bll.PlantService.Update(bllPlant);

            await _bll.SaveChangesAsync();


            return CreatedAtAction("GetPlant", new {id = bllPlant.Id}, bllPlant);
        }

        // POST: api/Plants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new plant
        /// </summary>
        /// <param name="plant">plant</param>
        /// <returns>added plant</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Plant>> PostPlant(Public.DTO.v1.Plant plant)
        {
            var bllPlant = _mapper.Map(plant);
            bllPlant!.AppUserId = User.GetUserId();
            var addedPlant = _bll.PlantService.Add(bllPlant);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPlant", new {id = addedPlant.Id}, addedPlant);

        }

        // DELETE: api/Plants/5
        /// <summary>
        /// delete a plant
        /// </summary>
        /// <param name="id">plant id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(Guid id)
        {
            var plant = await _bll.PlantService.RemoveAsync(id, User.GetUserId());

            if (plant == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
