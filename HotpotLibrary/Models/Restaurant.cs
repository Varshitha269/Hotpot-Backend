using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class Restaurant
{
    public int RestaurantID { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? PhNo { get; set; }

    public string Email { get; set; } = null!;

    public string? OperatingHours { get; set; }

    public string? AddressLine { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }
    [JsonIgnore]
    public virtual ICollection<FeedbackRating> FeedbackRatings { get; set; } = new List<FeedbackRating>();
    [JsonIgnore]
    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
