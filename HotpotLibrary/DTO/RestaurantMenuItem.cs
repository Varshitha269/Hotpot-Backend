using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.DTO
{
    public class RestaurantMenuItem
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string RestaurantDescription { get; set; }
        public string PhNo { get; set; }
        public string Email { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int MenuItemID { get; set; }
        public string ItemName { get; set; }
        public string MenuItemDescription { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string SpecialDietaryInfo { get; set; }
        public string TasteInfo { get; set; }
        public string NutritionalInfo { get; set; }
        public string AvailabilityTime { get; set; }
        public string ImageURL { get; set; }
        public double AverageRating { get; set; }
    }
}

