namespace DAO.DBModels;

public partial class Transaction
{
    public int Uid { get; set; }

    public double? Tscreation { get; set; }

    public double? Tstransaction { get; set; }

    public double? Amount { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? StateId { get; set; }

    public int? WalletId { get; set; }

    public int? PersonId { get; set; }
}
