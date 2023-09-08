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
    /// plant collection controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlantCollectionController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PlantCollectionMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PlantCollectionController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PlantCollectionMapper(autoMapper);
        }

        // GET: api/PlantCollections
        /// <summary>
        /// get all plant collections
        /// </summary>
        /// <returns>list of plant collections</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PlantCollection>>> GetPlantCollections()
        {
            var data = await 
                _bll.PlantCollectionService.AllAsync(User.GetUserId());

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/PlantCollections/5
        /// <summary>
        /// get plant collection by id
        /// </summary>
        /// <param name="id">plant collection id</param>
        /// <returns>plant collection</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.PlantCollection>> GetPlantCollection(Guid id)
        {
            var plantCollection = await _bll.PlantCollectionService.FindAsync(id, User.GetUserId());
            
            if (plantCollection == null)
            {
                return NotFound();
            }

            return _mapper.Map(plantCollection)!;
        }

        // PUT: api/PlantCollections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a plant collection
        /// </summary>
        /// <param name="id">plant collection id</param>
        /// <param name="plantCollection">plant collection</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantCollection(Guid id, Public.DTO.v1.PlantCollection plantCollection)
        {
            if (id != plantCollection.Id)
            {
                return BadRequest();
            }
            
            var bllPlantCollection = _mapper.Map(plantCollection);
            bllPlantCollection!.AppUserId = User.GetUserId();
            _bll.PlantCollectionService.Update(bllPlantCollection!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/PlantCollections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new plant collection
        /// </summary>
        /// <param name="plantCollection">plant collection</param>
        /// <returns>added plant collection</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.PlantCollection>> PostPlantCollection(Public.DTO.v1.PlantCollection plantCollection)
        {
            var bllPlantCollection = _mapper.Map(plantCollection);
            bllPlantCollection!.AppUserId = User.GetUserId();
            _bll.PlantCollectionService.Add(bllPlantCollection!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPlantCollection", new {id = plantCollection.Id}, plantCollection);

        }

        // DELETE: api/PlantCollections/5
        /// <summary>
        /// delete a plant collection
        /// </summary>
        /// <param name="id">plant collection id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantCollection(Guid id)
        {
            var plantCollection = await _bll.PlantCollectionService.RemoveAsync(id, User.GetUserId());

            if (plantCollection == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
