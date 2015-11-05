using CrmCoreLite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Brokers.Models
{
    [Table("Broker")]
    public class Broker : Person
    {
        [Display(Name ="Agency Name")]
        public string AgencyName { get; set; }
    }
}
