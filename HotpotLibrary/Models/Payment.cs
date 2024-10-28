using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class Payment
{
    public int PaymentID { get; set; }

    public int OrderID { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public decimal AmountPaid { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string TransactionStatus { get; set; } = null!;
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
}
