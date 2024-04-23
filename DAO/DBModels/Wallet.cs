using System;
using System.Collections.Generic;

namespace DAO.DBModels;

public partial class Wallet
{
    public int Uid { get; set; }

    public string? Name { get; set; }

    public double? Balance { get; set; }

    public int? IsCash { get; set; }

    public string? Color { get; set; }
}
