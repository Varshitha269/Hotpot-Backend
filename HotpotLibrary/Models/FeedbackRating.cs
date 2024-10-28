using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class FeedbackRating
{
    public int FeedbackRatingID { get; set; }

    public int UserID { get; set; }

    public int RestaurantID { get; set; }

    public string? Message { get; set; }

    public int Rating { get; set; }

    public DateTime CreatedDate { get; set; }
    [JsonIgnore]
    public virtual Restaurant Restaurant { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
