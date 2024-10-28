using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.DTO
{
    public class MenuItemDTO
    {
        public int MenuItemID { get; set; }
        public int MenuID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string SpecialDietaryInfo { get; set; }
        public string TasteInfo { get; set; }
        public string NutritionalInfo { get; set; }
        public string AvailabilityTime { get; set; }
        public bool IsInStock { get; set; }
        public string ImageUrl { get;  set; }
        
        public bool IsAvailable { get; set; }
        public decimal Discounts { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
