using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class MonthController : Controller
    {

        private readonly IAppUOW _uow;
        
        public MonthController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Month
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.MonthRepository.AllAsync();
            return View(vm);
        }

        // GET: Month/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var month = await _uow.MonthRepository.FindAsync(id.Value);
            if (month == null)
            {
                return NotFound();
            }

            return View(month);
        }

        // GET: Month/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Month/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonthNr,MonthName,Id")] Month month)
        {
            if (ModelState.IsValid)
            {
                month.Id = Guid.NewGuid();
                _uow.MonthRepository.Add(month);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(month);
        }

        // GET: Month/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var month = await _uow.MonthRepository.FindAsync(id.Value);
            if (month == null)
            {
                return NotFound();
            }
            return View(month);
        }

        // POST: Month/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MonthNr,MonthName,Id")] Month month)
        {
            if (id != month.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.MonthRepository.Update(month);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(month);
        }

        // GET: Month/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var month = await _uow.MonthRepository.FindAsync(id.Value);
            if (month == null)
            {
                return NotFound();
            }

            return View(month);
        }

        // POST: Month/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var month = await _uow.MonthRepository.FindAsync(id);
            if (month != null)
            {
                _uow.MonthRepository.Remove(month);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
