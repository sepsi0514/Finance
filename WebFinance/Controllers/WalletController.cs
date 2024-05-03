using DAO.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace WebFinance.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService<Wallet> _walletService;

        public WalletController(IWalletService<Wallet> walletService)
        {
            _walletService = walletService;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var wallet = await _walletService.GetWallets().FirstOrDefaultAsync();
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
            //if (ModelState.IsValid)
            //{
            //    _walletService.Add(new DAO.DBModels.Wallet
            //    {
            //        Name = wallet.Name,
            //        Balance = wallet.Balance,
            //        IsCash = 0,
            //        Color = wallet.Color
            //    });
            //    await _walletService.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            return View(wallet);
        }

        public async Task<IActionResult> Index() => View(await _walletService.GetWallets()
                .Select(wallet => new Models.Wallet
                {
                    Uid = wallet.Uid,
                    Name = wallet.Name,
                    Balance = wallet.Balance,
                    IsCash = wallet.IsCash == 1,
                    Color = wallet.Color
                }).ToListAsync());

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

            var wallet = await _walletService.GetWallets().FirstOrDefaultAsync(w => w.Uid == id);
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
            //if (id != wallet?.Uid)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var dbModel = new DAO.DBModels.Wallet()
            //        {
            //            Uid = wallet.Uid,
            //            Name = wallet.Name,
            //            Balance = wallet.Balance,
            //            IsCash = wallet.IsCash ? 1 : 0,
            //            Color = wallet.Color
            //        };

            //        _walletService.Update(dbModel);

            //        await _walletService.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!StudentExists(wallet.Uid)) return NotFound();
            //        else throw;
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(wallet);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dbWallet = await _walletService.GetWallets().FirstOrDefaultAsync(m => m.Uid == id);
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
            //var wallet = await _walletService.GetWallets().FirstOrDefaultAsync(w => w.Uid == id);
            //_walletService.Wallets.Remove(wallet);
            //await _walletService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id) => _walletService.GetWallets().Any(e => e.Uid == id);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_walletService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
