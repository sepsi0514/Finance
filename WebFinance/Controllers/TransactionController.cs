using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FireStoreDao;

namespace WebFinance.Controllers
{
    public class TransactionController : Controller
    {

        public FinanceDatasContext _financeDatasContext;

        public TransactionController(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }


        public async Task<IActionResult> Index()
        {
            return NotFound();

        }

        // POST: Wallet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Amount,Tstransaction,WalletId,Wallets")] Models.Transaction transaction)
        {
            long unixTime = ((DateTimeOffset)transaction.Tstransaction).ToUnixTimeSeconds();

            return NotFound();
        }

        public IActionResult Create()
        {
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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
