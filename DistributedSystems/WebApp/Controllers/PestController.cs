using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PestController : Controller
    {

        private readonly IAppUOW _uow;
        
        public PestController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Pest
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PestRepository.AllAsync();
            return View(vm);
        }

        // GET: Pest/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest = await _uow.PestRepository.FindAsync(id.Value);
            if (pest == null)
            {
                return NotFound();
            }

            return View(pest);
        }

        // GET: Pest/Create
        public IActionResult Create()
        {
            ViewData["PestSeverityId"] = new SelectList(_uow.PestSeverityRepository.AllAsync().Result, "Id", "PestSeverityName");
            ViewData["PestTypeId"] = new SelectList(_uow.PestTypeRepository.AllAsync().Result, "Id", "PestTypeName");
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName");
            return View();
        }

        // POST: Pest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PestComment,PestDiscoveryTime,PlantId,PestTypeId,PestSeverityId,Id")] Pest pest)
        {
            if (ModelState.IsValid)
            {
                pest.Id = Guid.NewGuid();
                _uow.PestRepository.Add(pest);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PestSeverityId"] = new SelectList(_uow.PestSeverityRepository.AllAsync().Result, "Id", "PestSeverityName", pest.PestSeverityId);
            ViewData["PestTypeId"] = new SelectList(_uow.PestTypeRepository.AllAsync().Result, "Id", "PestTypeName", pest.PestTypeId);
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", pest.PlantId);

            return View(pest);
        }

        // GET: Pest/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest = await _uow.PestRepository.FindAsync(id.Value);
            if (pest == null)
            {
                return NotFound();
            }
            ViewData["PestSeverityId"] = new SelectList(_uow.PestSeverityRepository.AllAsync().Result, "Id", "PestSeverityName", pest.PestSeverityId);
            ViewData["PestTypeId"] = new SelectList(_uow.PestTypeRepository.AllAsync().Result, "Id", "PestTypeName", pest.PestTypeId);
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", pest.PlantId);
            return View(pest);
        }

        // POST: Pest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PestComment,PestDiscoveryTime,PlantId,PestTypeId,PestSeverityId,Id")] Pest pest)
        {
            if (id != pest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PestRepository.Update(pest);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["PestSeverityId"] = new SelectList(_uow.PestSeverityRepository.AllAsync().Result, "Id", "PestSeverityName", pest.PestSeverityId);
            ViewData["PestTypeId"] = new SelectList(_uow.PestTypeRepository.AllAsync().Result, "Id", "PestTypeName", pest.PestTypeId);
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", pest.PlantId);
            return View(pest);
        }

        // GET: Pest/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pest = await _uow.PestRepository.FindAsync(id.Value);
            if (pest == null)
            {
                return NotFound();
            }

            return View(pest);
        }

        // POST: Pest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pest = await _uow.PestRepository.FindAsync(id);
            if (pest != null)
            {
                _uow.PestRepository.Remove(pest);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
