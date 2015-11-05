using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmCoreLite.Areas.Brokers.Models
{
    public enum InsentiveStatus
    {
        RELEASED,ONHOLD,CANCELLED
    }
    public class Insentive
    {
        public int ID { get; set; }

        [Display(Name = "Insentive Amount")]
        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        public decimal InsentiveAmount { get; set; }

        public InsentiveStatus? InsentiveStatus { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public int? AgentID { get; set; }
        public virtual Agent Agent { get; set; }
    }
}
