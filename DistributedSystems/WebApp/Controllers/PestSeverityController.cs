using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PestSeverityController : Controller
    {

        private readonly IAppUOW _uow;
        
        public PestSeverityController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: PestSeverity
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PestSeverityRepository.AllAsync();
            return View(vm);
        }

        // GET: PestSeverity/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pestSeverity = await _uow.PestSeverityRepository.FindAsync(id.Value);
            if (pestSeverity == null)
            {
                return NotFound();
            }

            return View(pestSeverity);
        }

        // GET: PestSeverity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PestSeverity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PestSeverityName,Id")] PestSeverity pestSeverity)
        {
            if (ModelState.IsValid)
            {
                pestSeverity.Id = Guid.NewGuid();
                _uow.PestSeverityRepository.Add(pestSeverity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pestSeverity);
        }

        // GET: PestSeverity/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pestSeverity = await _uow.PestSeverityRepository.FindAsync(id.Value);
            if (pestSeverity == null)
            {
                return NotFound();
            }
            return View(pestSeverity);
        }

        // POST: PestSeverity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PestSeverityName,Id")] PestSeverity pestSeverity)
        {
            if (id != pestSeverity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PestSeverityRepository.Update(pestSeverity);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(pestSeverity);
        }

        // GET: PestSeverity/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pestSeverity = await _uow.PestSeverityRepository.FindAsync(id.Value);
            if (pestSeverity == null)
            {
                return NotFound();
            }

            return View(pestSeverity);
        }

        // POST: PestSeverity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pestSeverity = await _uow.PestSeverityRepository.FindAsync(id);
            if (pestSeverity != null)
            {
                _uow.PestSeverityRepository.Remove(pestSeverity);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
