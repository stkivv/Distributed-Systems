using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IAppUOW _uow;
        
        public PhotoController(IAppUOW uow)
        {
            _uow = uow;
        }
        

        // GET: Photo
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PhotoRepository.AllAsync();
            return View(vm);
        }

        // GET: Photo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _uow.PhotoRepository.FindAsync(id.Value);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: Photo/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName");
            return View();
        }

        // POST: Photo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageUrl,ImageDescription,PlantId,Id")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                photo.Id = Guid.NewGuid();
                _uow.PhotoRepository.Add(photo);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", photo.PlantId);
            return View(photo);
        }

        // GET: Photo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _uow.PhotoRepository.FindAsync(id.Value);
            if (photo == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", photo.PlantId);
            return View(photo);
        }

        // POST: Photo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ImageUrl,ImageDescription,PlantId,Id")] Photo photo)
        {
            if (id != photo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PhotoRepository.Update(photo);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", photo.PlantId);
            return View(photo);
        }

        // GET: Photo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _uow.PhotoRepository.FindAsync(id.Value);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var photo = await _uow.PhotoRepository.FindAsync(id);
            if (photo != null)
            {
                _uow.PhotoRepository.Remove(photo);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
