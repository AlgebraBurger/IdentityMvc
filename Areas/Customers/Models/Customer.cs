using CrmCoreLite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Customers.Models
{
    public enum EmploymentStatus
    {
        SelfEmployed, Employed, Retired
    }

    [Table("Customer")]
    public class Customer : Person
    {

        [StringLength(30)]
        [Display(Name = "Acc No")]
        public string AccountNo { get; set; }

        [Display(Name = "Employment Status")]
        public EmploymentStatus? EmploymentStatus { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(20)]
        public string CompanyName { get; set; }

        [Display(Name = "Nature of Business")]
        [StringLength(20)]
        public string NatureofBusiness { get; set; }

        [Required]
        [StringLength(20)]
        public string Occupation { get; set; }

        [Display(Name = "Company Address")]
        [StringLength(100)]
        public string CompanyAddress { get; set; }

        [Display(Name = "Company Tel No")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###)-###-####}")]
        public int? CompanyTelephoneNumbers { get; set; }

        [Display(Name = "Years in Company")]
        [Range(1, 100)]
        public int? YearsWithCompany { get; set; }

        [Display(Name = "Monthly Income")]
        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        public decimal? MonthlyIncome { get; set; }


        [StringLength(30)]
        [Display(Name = "TIN")]
        public string TaxIdentificationNumber { get; set; }

        [StringLength(30)]
        [Display(Name = "Passport Number")]
        public string Passport { get; set; }

        [StringLength(30)]
        [Display(Name = "Valid Government ID")]
        public string ValidGovernmentID { get; set; }

        [StringLength(30)]
        [Display(Name = "Place Issued")]
        public string PlaceIssue { get; set; }

        [Display(Name = "Date Issued")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateIssue { get; set; }
        

    }
}



