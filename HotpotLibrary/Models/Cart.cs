using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class Cart
{
    public int CartID { get; set; }

    public int UserID { get; set; }

    public DateTime CreatedDate { get; set; }

    public int MenuItemID { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
    [JsonIgnore]
    public virtual MenuItem MenuItem { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
