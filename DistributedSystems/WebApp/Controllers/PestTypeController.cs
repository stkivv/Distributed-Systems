using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PestTypeController : Controller
    {

        private readonly IAppUOW _uow;
        
        public PestTypeController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: PestType
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PestTypeRepository.AllAsync();
            return View(vm);
        }

        // GET: PestType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pestType = await _uow.PestTypeRepository.FindAsync(id.Value);
            if (pestType == null)
            {
                return NotFound();
            }

            return View(pestType);
        }

        // GET: PestType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PestType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PestTypeName,Id")] PestType pestType)
        {
            if (ModelState.IsValid)
            {
                pestType.Id = Guid.NewGuid();
                _uow.PestTypeRepository.Add(pestType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pestType);
        }

        // GET: PestType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pestType = await _uow.PestTypeRepository.FindAsync(id.Value);
            if (pestType == null)
            {
                return NotFound();
            }
            return View(pestType);
        }

        // POST: PestType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PestTypeName,Id")] PestType pestType)
        {
            if (id != pestType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PestTypeRepository.Update(pestType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pestType);
        }

        // GET: PestType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pestType = await _uow.PestTypeRepository.FindAsync(id.Value);
            if (pestType == null)
            {
                return NotFound();
            }

            return View(pestType);
        }

        // POST: PestType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pestType = await _uow.PestTypeRepository.FindAsync(id);
            if (pestType != null)
            {
                _uow.PestTypeRepository.Remove(pestType);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
