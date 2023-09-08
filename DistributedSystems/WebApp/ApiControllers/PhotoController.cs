using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// controller for photos
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PhotoController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.PhotoMapper _mapper;
        
        /// <summary>
        /// constructor that takes bll and mapper
        /// </summary>
        /// <param name="bll">bll</param>
        /// <param name="autoMapper">mapper</param>
        public PhotoController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new Public.DTO.Mappers.PhotoMapper(autoMapper);
        }

        // GET: api/Photos
        /// <summary>
        /// get all photos
        /// </summary>
        /// <returns>list of photos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.Photo>>> GetPhotos()
        {
            var data = await 
                _bll.PhotoService.AllAsync();

            return data
                .Select(e => _mapper.Map(e)!)
                .ToList();
        }

        // GET: api/Photos/5
        /// <summary>
        /// get a photo by id
        /// </summary>
        /// <param name="id">photo id</param>
        /// <returns>photo object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.Photo>> GetPhoto(Guid id)
        {
            var photo = await _bll.PhotoService.FindAsync(id);
            
            if (photo == null)
            {
                return NotFound();
            }

            return _mapper.Map(photo)!;
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// update a photo
        /// </summary>
        /// <param name="id">photo id</param>
        /// <param name="photo">photo object</param>
        /// <returns>noContent</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(Guid id, Public.DTO.v1.Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }
            
            var bllPhoto = _mapper.Map(photo);

            _bll.PhotoService.Update(bllPhoto!);

            await _bll.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// add a new photo
        /// </summary>
        /// <param name="photo">photo object</param>
        /// <returns>added photo</returns>
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.Photo>> PostPhoto(Public.DTO.v1.Photo photo)
        {
            var bllPhoto = _mapper.Map(photo);
            _bll.PhotoService.Add(bllPhoto!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPhoto", new {id = photo.Id}, photo);

        }

        // DELETE: api/Photos/5
        /// <summary>
        /// delete a photo
        /// </summary>
        /// <param name="id">photo id</param>
        /// <returns>noContent</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(Guid id)
        {
            var photo = await _bll.PhotoService.RemoveAsync(id);

            if (photo == null) return NotFound();
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
