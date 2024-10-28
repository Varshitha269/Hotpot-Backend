using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class MenuItem
{
    public int MenuItemID { get; set; }

    public int MenuID { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public string? SpecialDietaryInfo { get; set; }

    public string? TasteInfo { get; set; }

    public string? NutritionalInfo { get; set; }

    public string? AvailabilityTime { get; set; }

    public bool? IsInStock { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsAvailable { get; set; }

    public decimal Discounts { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Menu Menu { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
