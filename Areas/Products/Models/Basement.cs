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
    public enum BaseNo
    {
        one,two,three
    }
    [Table("Basement")]
    public class Basement : Product
    {
     
        [Display(Name = "Unit No.")]
        public string UnitNo { get; set; }

        public BaseNo? BaseNo { get; set; }

        [Display(Name = "Unit Area (sq. m)")]
        public string UnitArea { get; set; }

        [Display(Name = "Total Contract Price")]
        [DataType(DataType.Currency)]
        public decimal TotalContractPrice { get; set; }
             
    }
}
