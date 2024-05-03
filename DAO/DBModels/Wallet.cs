namespace DAO.DBModels;

public class Wallet
{
    public int Uid { get; set; }

    public string? Name { get; set; }

    public double? Balance { get; set; }

    public int? IsCash { get; set; }

    public string? Color { get; set; }
}
