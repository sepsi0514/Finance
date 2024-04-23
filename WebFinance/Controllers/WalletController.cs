using DAO.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebFinance.Controllers
{
    public class WalletController : Controller
    {
        private readonly FinanceDatasContext _financeDatasContext;

        public WalletController(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var wallet = await _financeDatasContext.Wallets.FirstOrDefaultAsync();
            if (wallet == null) return NotFound();

            var viewModel = new Models.Wallet()
            {
                Uid = wallet.Uid,
                Name = wallet.Name,
                Balance = wallet.Balance,
                IsCash = wallet.IsCash == 1,
                Color = wallet.Color
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Wallet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Balance,IsCash,Color")] Models.Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _financeDatasContext.Add(new DAO.DBModels.Wallet
                {
                    Name = wallet.Name,
                    Balance = wallet.Balance,
                    IsCash = 0,
                    Color = wallet.Color
                });
                await _financeDatasContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(wallet);
        }

        public async Task<IActionResult> Index()
        {
            var asd = _financeDatasContext.Wallets.ToList();
            return View(await _financeDatasContext.Wallets
                .Select(wallet => new Models.Wallet
                {
                    Uid = wallet.Uid,
                    Name = wallet.Name,
                    Balance = wallet.Balance,
                    IsCash = wallet.IsCash == 1,
                    Color = wallet.Color
                }).ToListAsync());
        }

        public IActionResult Welcome(string name, int numtimes)
        {
            ViewData["Message"] = $"Hello {name}";
            ViewData["NumTimes"] = numtimes;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var wallet = await _financeDatasContext.Wallets.FindAsync(id);
            if (wallet == null) return NotFound();

            if (wallet == null) return NotFound();

            var viewModel = new Models.Wallet()
            {
                Uid = wallet.Uid,
                Name = wallet.Name,
                Balance = wallet.Balance,
                IsCash = wallet.IsCash == 1,
                Color = wallet.Color
            };

            return View(viewModel);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Uid,Name,Balance,IsCash,Color")] Models.Wallet wallet)
        {
            if (id != wallet?.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbModel = new DAO.DBModels.Wallet()
                    {
                        Uid = wallet.Uid,
                        Name = wallet.Name,
                        Balance = wallet.Balance,
                        IsCash = wallet.IsCash ? 1 : 0,
                        Color = wallet.Color
                    };

                    _financeDatasContext.Update(dbModel);

                    await _financeDatasContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(wallet.Uid)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wallet);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dbWallet = await _financeDatasContext.Wallets.FirstOrDefaultAsync(m => m.Uid == id);
            if (dbWallet == null) return NotFound();

            var viewModel = new Models.Wallet()
            {
                Uid = dbWallet.Uid,
                Name = dbWallet.Name,
                Balance = dbWallet.Balance,
                IsCash = dbWallet.IsCash == 1
            };

            return View(viewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wallet = await _financeDatasContext.Wallets.FindAsync(id);
            _financeDatasContext.Wallets.Remove(wallet);
            await _financeDatasContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id) => _financeDatasContext.Wallets.Any(e => e.Uid == id);
    }
}
