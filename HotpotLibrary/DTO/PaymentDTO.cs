using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.DTO
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
    }

}
