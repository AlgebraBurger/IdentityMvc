using CrmCoreLite.Areas.Brokers.Models;
using CrmCoreLite.Areas.Products.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Units.Models
{
    public enum UnitType
    {
        ONEBR, TWOBR,STUDIO, ONEBR_LOFT_TANDB, TWOBR_TWOCR_LOFT_TANDB, TWOBR_LOFT_TANDB
    }
    public enum UnitStatus
    {
        AVAILABLE, RESERVED ,SOLD
    }

    public enum uViewType
    {
        CSV, CSV_MV,CSV_TV,MV, MV_TV, TV,
    }
    [Table("Unit")]
    public class Unit : Product
    {
        
        [Display(Name = "Unit No.")]
        [StringLength(30)]
        public string UnitNo { get; set; }

        [Display(Name = "Unit Type")]
        public UnitType? UnitType { get; set; }

        [Display(Name = "View Type")]
        public uViewType? uViewType { get; set; }
        
        [Display(Name = "Unit Area (sq. m)")]
        public decimal UnitArea { get; set; }

        [Display(Name = "Balcony (sq. m)")]
        public decimal Balcony { get; set; }

        [Display(Name = "Total Area (sq. m)")]
        public decimal TotalArea { get; set; }

        [Display(Name = "Total List Price")]
        [DataType(DataType.Currency)]
        public decimal TotalListPrice { get; set; }

        [Display(Name = "Transfer and Misc Fees")]
        [DataType(DataType.Currency)]
        public decimal TransferMiscFees { get; set; }

        [Display(Name = "Total Contract Price")]
        [DataType(DataType.Currency)]
        public decimal TotalContractPrice { get; set; }

  
        [Display(Name = "Unit Status")]
        public UnitStatus? UnitStatus { get; set; }
       


        
    }
}
