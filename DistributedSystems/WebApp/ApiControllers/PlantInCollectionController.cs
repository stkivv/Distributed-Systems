using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// plant in collection controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlantInCollectionController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PlantInCollectionMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PlantInCollectionController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PlantInCollectionMapper(autoMapper);
        }

        // GET: api/PlantInCollections
        /// <summary>
        /// get all plant in collections
        /// </summary>
        /// <returns>list of plant in collections</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PlantInCollection>>> GetPlantInCollections()
        {
            var data = await 
                _bll.PlantInCollectionService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/PlantInCollections/5
        /// <summary>
        /// get a plant in collection by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>plant in collection</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.PlantInCollection>> GetPlantInCollection(Guid id)
        {
            var plantInCollection = await _bll.PlantInCollectionService.FindAsync(id);
            
            if (plantInCollection == null)
            {
                return NotFound();
            }

            return _mapper.Map(plantInCollection)!;
        }

        // PUT: api/PlantInCollections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a plant in collection
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="plantInCollection">plant in collection object</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantInCollection(Guid id, Public.DTO.v1.PlantInCollection plantInCollection)
        {
            if (id != plantInCollection.Id)
            {
                return BadRequest();
            }
            
            var bllPlantInCollection = _mapper.Map(plantInCollection);

            _bll.PlantInCollectionService.Update(bllPlantInCollection!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/PlantInCollections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new plant in collection
        /// </summary>
        /// <param name="plantInCollection">plant in collection</param>
        /// <returns>added plant in collection</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.PlantInCollection>> PostPlantInCollection(Public.DTO.v1.PlantInCollection plantInCollection)
        {
            var bllPlantInCollection = _mapper.Map(plantInCollection);
            _bll.PlantInCollectionService.Add(bllPlantInCollection!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPlantInCollection", new {id = plantInCollection.Id}, plantInCollection);

        }

        // DELETE: api/PlantInCollections/5
        /// <summary>
        /// delete a plant in collection
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantInCollection(Guid id)
        {
            var plantInCollection = await _bll.PlantInCollectionService.RemoveAsync(id);

            if (plantInCollection == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
