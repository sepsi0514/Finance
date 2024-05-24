using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebFinance.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        //private readonly IWalletService<Wallet> _walletService;

        //public WalletController(IWalletService<Wallet> walletService)
        //{
        //    _walletService = walletService;
        //}

        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null) return NotFound();

            //var wallet = await _walletService.GetWallets().FirstOrDefaultAsync();
            //if (wallet == null) return NotFound();

            //var viewModel = new Models.Wallet()
            //{
            //    Uid = wallet.Uid,
            //    Name = wallet.Name,
            //    Balance = wallet.Balance,
            //    IsCash = wallet.IsCash == 1,
            //    Color = wallet.Color
            //};

            return NotFound();
            //return View(viewModel);
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

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.TryGetValue("user", out var userByte);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            return NotFound();
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
            return NotFound();
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
