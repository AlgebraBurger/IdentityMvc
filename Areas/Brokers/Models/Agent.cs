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
    [Table("Agent")]
    public class Agent : Person
    {
        [Display(Name = "Agent No")]
        [StringLength(30)]
        public string AgentNo { get; set; }

        public string Agency { get; set; }

        [Display(Name = "Broker")]
        public int? BrokerID { get; set; }
        public virtual Broker Broker { get; set; }

    }
}
