using DAO.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFinance.Models;

namespace WebFinance.Controllers
{
    public class TransactionController : Controller
    {
        private FinanceDatasContext _financeDatasContext;

        public TransactionController(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _financeDatasContext.Transactions
                .Select(transaction => new Models.Transaction
                {
                    Uid = transaction.Uid,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    Tstransaction = UnixTimeStampToDateTime(transaction.Tstransaction ?? 0)
                }).ToListAsync());
        }

        // POST: Wallet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Amount,Tstransaction,WalletId,Wallets")] Models.Transaction transaction)
        {
            long unixTime = ((DateTimeOffset)transaction.Tstransaction).ToUnixTimeSeconds();

            _financeDatasContext.Add(new DAO.DBModels.Transaction
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                Tstransaction = unixTime,
                WalletId = transaction.WalletId
            });
            await _financeDatasContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            var wallets = _financeDatasContext.Wallets
                .Select(wallet => new SelectListItem
                {
                    Text = wallet.Name,
                    Value = wallet.Uid.ToString()
                }).ToList();

            var transaction = new Models.Transaction
            {
                Tstransaction = DateTime.Now,
                Wallets = wallets
            };

            return View(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _financeDatasContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
