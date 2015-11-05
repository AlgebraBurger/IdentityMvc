using CrmCoreLite.Areas.Brokers.Models;
using CrmCoreLite.Areas.Customers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Orders.Models
{
    public enum ContractStatus
    {
        APPROVED, PENDING, DECLINED
    }
    public enum PaymentType
    {
        EASY, MEDIUM, HARD, BRUTAL
    }
    
    public class Order
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Display(Name = "Order No")]
        public string OrderNo{ get; set; }

       

        [Display(Name = "Payment Type")]
        public PaymentType? PaymentType { get; set; }

        [Display(Name = "Contract Status")]
        public ContractStatus? ContractStatus { get; set; }

        
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        
        public int AgentID { get; set; }
        public virtual Agent Agent { get;set;}

        

        [Display(Name = "Reservation Fee")]
        [Range(1, 1000000000)]
        [DataType(DataType.Currency)]
        public decimal ReservationFee { get; set; }

        [Display(Name = "Total Contract Price")]
        [Range(1, 1000000000)]
        [DataType(DataType.Currency)]
        public decimal TotalOrderAmount { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }
    }
}
