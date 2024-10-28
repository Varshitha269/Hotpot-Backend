using HotpotLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IRestaurantMenuItem
    {
        public IEnumerable<RestaurantMenuItem> GetRestaurantsWithMenuItem(string menuItemName);
    }
}
