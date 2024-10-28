using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.DTO
{
    public class RestaurantDTO
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhNo { get; set; }
        public string Email { get; set; }
        public string OperatingHours { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

}
