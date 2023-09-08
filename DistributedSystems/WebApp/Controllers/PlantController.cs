using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
using Helpers.Base;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PlantController : Controller
    {
        private readonly IAppUOW _uow;
        
        public PlantController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Plant
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PlantRepository.AllAsync(User.GetUserId());
            return View(vm);
        }

        // GET: Plant/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _uow.PlantRepository.FindAsync(id.Value);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plant/Create
        public IActionResult Create()
        {
            ViewData["SizeCategoryId"] = new SelectList(_uow.SizeCategoryRepository.AllAsync().Result, "Id", "SizeName");
            return View();
        }

        // POST: Plant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantName,Description,PlantFamily,ScientificName,AppUserId,SizeCategoryId,Id")] Plant plant)
        {
            plant.AppUserId = User.GetUserId();
            if (ModelState.IsValid)
            {
                plant.Id = Guid.NewGuid();
                _uow.PlantRepository.Add(plant);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SizeCategoryId"] = new SelectList(_uow.SizeCategoryRepository.AllAsync().Result, "Id", "SizeName", plant.SizeCategoryId);
            return View(plant);
        }

        // GET: Plant/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _uow.PlantRepository.FindAsync(id.Value);
            if (plant == null)
            {
                return NotFound();
            }
            ViewData["SizeCategoryId"] = new SelectList(_uow.SizeCategoryRepository.AllAsync().Result, "Id", "SizeName", plant.SizeCategoryId);

            return View(plant);
        }

        // POST: Plant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PlantName,Description,PlantFamily,ScientificName,AppUserId,SizeCategoryId,Id")] Plant plant)
        {
            plant.AppUserId = User.GetUserId();
            if (id != plant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PlantRepository.Update(plant);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["SizeCategoryId"] = new SelectList(_uow.SizeCategoryRepository.AllAsync().Result, "Id", "SizeName", plant.SizeCategoryId);

            return View(plant);
        }

        // GET: Plant/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _uow.PlantRepository.FindAsync(id.Value);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var plant = await _uow.PlantRepository.FindAsync(id);
            if (plant != null)
            {
                _uow.PlantRepository.Remove(plant);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
