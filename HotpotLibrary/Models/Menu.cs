using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotpotLibrary.Models;

public class Menu
{
    public int MenuID { get; set; }

    public int RestaurantID { get; set; }

    public string MenuName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }
    [JsonIgnore]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
