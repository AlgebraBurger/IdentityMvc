using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrmCoreLite.Models
{
    public enum CivilStatus
    {
        Single,Married, Divorced, Widowed
    }
    
    public class Person
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Index ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Invalid Characters")]
        [Display(Name="Firstname")]
        public string Firstname { get; set; }

        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Invalid Characters")]
        [Display(Name = "Middlename")]
        public string Middlename { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Invalid Characters")]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Display(Name ="Full Name")]
        public string FullName
        {
            get
            {
                return Firstname + " " + Middlename + " " + Lastname;
            }
        }
        
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        
        [Display(Name = "Tel No")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###)-###-####}")]
        public int Telephone { get; set; }

        [Required]        
        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###)-###-####}")]
        public int Mobile { get; set; }

        


        [Display(Name = "Birth Place")]
        [StringLength(100)]
        public string Birthplace { get; set; }

        public int Age
        {
            get {
                DateTime today = DateTime.Today;
                int age = today.Year - BirthDay.Year;
                if (BirthDay > today.AddYears(-age)) age--;
                return age;                
            }
        }
        
        [Required]
        [StringLength(100)]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Required]        
        [Display(Name = "Nationality")]
        [StringLength(20)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Invalid Characters")]        
        public string Nationality { get; set; }

        [Display(Name = "Civil Status")]
        public CivilStatus CivilStatus { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Account Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }


    }


    

}
