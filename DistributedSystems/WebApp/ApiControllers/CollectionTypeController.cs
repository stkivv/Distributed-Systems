using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for collection types
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CollectionTypeController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.CollectionTypeMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public CollectionTypeController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.CollectionTypeMapper(autoMapper);
        }

        // GET: api/CollectionTypes
        /// <summary>
        /// get all collection types
        /// </summary>
        /// <returns>list of collection types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.CollectionType>>> GetCollectionTypes()
        {
            var data = await 
                _bll.CollectionTypeService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/CollectionTypes/5
        /// <summary>
        /// get collection type by id
        /// </summary>
        /// <param name="id">collection type id</param>
        /// <returns>collection type</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.CollectionType>> GetCollectionType(Guid id)
        {
            var collectionType = await _bll.CollectionTypeService.FindAsync(id);
            
            if (collectionType == null)
            {
                return NotFound();
            }

            return _mapper.Map(collectionType)!;
        }

        // PUT: api/CollectionTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a collection type
        /// </summary>
        /// <param name="id">collection type id</param>
        /// <param name="collectionType">collection type object</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollectionType(Guid id, Public.DTO.v1.CollectionType collectionType)
        {
            if (id != collectionType.Id)
            {
                return BadRequest();
            }
            
            var bllCollectionType = _mapper.Map(collectionType);

            _bll.CollectionTypeService.Update(bllCollectionType!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/CollectionTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new collection type
        /// </summary>
        /// <param name="collectionType">new object to add</param>
        /// <returns>added collectionType object</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.CollectionType>> PostCollectionType(Public.DTO.v1.CollectionType collectionType)
        {
            var bllCollectionType = _mapper.Map(collectionType);
            _bll.CollectionTypeService.Add(bllCollectionType!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCollectionType", new {id = collectionType.Id}, collectionType);

        }

        // DELETE: api/CollectionTypes/5
        /// <summary>
        /// delete collection type
        /// </summary>
        /// <param name="id">collection type id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectionType(Guid id)
        {
            var collectionType = await _bll.CollectionTypeService.RemoveAsync(id);

            if (collectionType == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
