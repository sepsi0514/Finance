using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces.Models;
using System.Text;
using WebFinance.Models;

namespace WebFinance.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

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
        public async Task<IActionResult> Create([Bind("Name,Balance,IsCash,Color")] Wallet wallet)
        {
            _walletService.CreateWallet(new WalletData(null, wallet.Name, wallet.Balance ?? 0, wallet.Color, wallet.IsCash));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _walletService.GetAll().Select(w => new Wallet() { Balance = w.Balance, Color = w.Color, IsCash = w.IsCash, Name = w.Name, Uid = w.uid}).ToListAsync());

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
        public async Task<IActionResult> Delete(string? id)
        {
            _walletService.Delete(id);
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
