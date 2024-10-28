using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class User
{
    public int UserID { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? PhNo { get; set; }

    public string? AddressLine { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    [JsonIgnore]
    public virtual ICollection<FeedbackRating> FeedbackRatings { get; set; } = new List<FeedbackRating>();
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
