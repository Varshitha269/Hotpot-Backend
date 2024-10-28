using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Repository
{
    public class RestaurantMenuItemRepository : IRestaurantMenuItem {

        private readonly AppDbContext _context;
        public RestaurantMenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RestaurantMenuItem> GetRestaurantsWithMenuItem(string menuItemName)
        {
            string query = @"
    SELECT r.RestaurantID, r.Name, 
           CAST(r.Description AS VARCHAR(MAX)) AS RestaurantDescription, 
           r.PhNo, r.Email, r.AddressLine, r.City, r.State, r.PostalCode, r.Country,
           mi.MenuItemID, mi.ItemName, 
           CAST(mi.Description AS VARCHAR(MAX)) AS MenuItemDescription, 
           mi.Category, mi.Price, 
           CAST(mi.SpecialDietaryInfo AS VARCHAR(MAX)) AS SpecialDietaryInfo, 
           CAST(mi.TasteInfo AS VARCHAR(MAX)) AS TasteInfo, 
           CAST(mi.NutritionalInfo AS VARCHAR(MAX)) AS NutritionalInfo, 
           mi.AvailabilityTime, 
           CAST(mi.ImageURL AS VARCHAR(MAX)) AS ImageURL,
           CAST(0 AS FLOAT) AS AverageRating -- Ensure it's cast as FLOAT
    FROM Restaurants r
    JOIN Menu m ON r.RestaurantID = m.RestaurantID
    JOIN MenuItems mi ON m.MenuID = mi.MenuID
    WHERE mi.ItemName LIKE @MenuItemName 
      AND mi.IsAvailable = 1
    GROUP BY r.RestaurantID, r.Name, 
             CAST(r.Description AS VARCHAR(MAX)), 
             r.PhNo, r.Email, r.AddressLine, r.City, r.State, r.PostalCode, r.Country,
             mi.MenuItemID, mi.ItemName, 
             CAST(mi.Description AS VARCHAR(MAX)), 
             mi.Category, mi.Price, 
             CAST(mi.SpecialDietaryInfo AS VARCHAR(MAX)), 
             CAST(mi.TasteInfo AS VARCHAR(MAX)), 
             CAST(mi.NutritionalInfo AS VARCHAR(MAX)), 
             mi.AvailabilityTime, 
             CAST(mi.ImageURL AS VARCHAR(MAX));
";





            // Use FromSqlRaw to execute the raw SQL query
            return _context.RestaurantsMenuItems
                           .FromSqlRaw(query, new SqlParameter("@MenuItemName", "%" + menuItemName + "%"))
                           .ToList();


        }
    }
}
