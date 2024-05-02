using System.ComponentModel.DataAnnotations;

namespace WebFinance.Models
{
    public class Wallet
    {
        public int Uid { get; set; }

        public string? Name { get; set; }

        public double? Balance { get; set; }

        public Boolean IsCash { get; set; }

        public  string Color { get; set; }
    }
}
