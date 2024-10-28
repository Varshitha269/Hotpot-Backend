using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class OrderDetail
{
    public int OrderDetailID { get; set; }

    public int OrderID { get; set; }

    public int MenuItemID { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? Subtotal { get; set; }
    [JsonIgnore]
    public virtual MenuItem MenuItem { get; set; } = null!;
    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
}
