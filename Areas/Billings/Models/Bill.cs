
using CrmCoreLite.Areas.Customers.Models;
using CrmCoreLite.Areas.Orders.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Billings.Models
{
    public enum PaymentClass
    {
        BANK,PDC,CASH,CREDITCARD,DEBITCARD
    }
    public enum BillStatus
    {
        PAID, PROCESSING, OPEN, OVERDUE
    }
    public class Bill
    {
        public int ID { get; set; }

        [Display(Name = "Bill No")]
        [StringLength(30)]
        public string BillNo { get; set; }

        [Display(Name = "Bill Amount")]
        [DataType(DataType.Currency)]
        public decimal BillAmount { get; set; }


        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        [Display(Name ="Bill Code")]
        public string BillCode{ get; set; }

        [Display(Name = "Customer")]
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Display(Name = "Billing Remark")]
        [StringLength(50)]
        public string BillRemarks { get; set; }


        [Display(Name = "Bill Due")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BillDue { get; set; }

        [Display(Name = "Bill Created")]                
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Bill Status")]
        public BillStatus? BillStatus { get; set; }

        [Display(Name = "Payment Type")]
        public PaymentClass? PaymentClass { get; set; }

        [Display(Name = "Payment Source")]
        [StringLength(50)]
        public string PaymentSource { get; set; }

        [Display(Name = "Payment Amount")]
        [DataType(DataType.Currency)]
        public decimal? PaymentAmount { get; set; }

        [Display(Name = "Payment Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentCreated { get; set; }



    }
}
