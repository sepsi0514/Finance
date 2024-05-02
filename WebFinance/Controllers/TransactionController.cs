using DAO.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebFinance.Controllers
{
    public class TransactionController : Controller
    {
        private FinanceDatasContext _financeDatasContext;

        public TransactionController(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }

        // POST: Wallet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Amount,Tstransaction,WalletId")] Models.Transaction transaction)
        {
            if (ModelState.IsValid)
            {
               
            }

            var wallets = _financeDatasContext.Wallets
               .Select(wallet => new SelectListItem
               {
                   Text = wallet.Name,
                   Value = wallet.Uid.ToString()
               }).ToList();

            transaction = new Models.Transaction
            {
                Wallets = wallets
            };

            return View(transaction);
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
    }
}
