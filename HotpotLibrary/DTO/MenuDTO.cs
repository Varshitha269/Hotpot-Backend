using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.DTO
{
    public class MenuDTO
    {
        public int MenuID { get; set; }
        public int RestaurantID { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

}
