using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class Order
{
    public int OrderID { get; set; }
    public int UserID { get; set; }  // Foreign key reference to User
    public int RestaurantID { get; set; }  // Foreign key reference to Restaurant
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentStatus { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? CreatedDate { get; set; }

    // Navigation properties
    [JsonIgnore]  // Ignore these during serialization
    public virtual Restaurant Restaurant { get; set; }
    [JsonIgnore]  // Ignore these during serialization
    public virtual User User { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

}
