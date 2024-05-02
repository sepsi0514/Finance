using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebFinance.Models
{
    public class Transaction
    {
        public int Uid { get; set; }

        [Display(Name = "Transaction created")]
        [DataType(DataType.Date)]
        public DateTime? Tscreation { get; set; }

        [Display(Name = "Transaction completed")]
        [DataType(DataType.Date)]
        public DateTime? Tstransaction { get { return _tstransaction == null ? DateTime.Now : _tstransaction; } set { _tstransaction = value; } }

        private DateTime? _tstransaction { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        public int? StateId { get; set; }

        public int? WalletId { get; set; }

        public int? PersonId { get; set; }

        [AllowNull]
        public IEnumerable<SelectListItem> Wallets { get; set; }
    }
}
