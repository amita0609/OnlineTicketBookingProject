using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTicketData.Models
{
    public class TicketBooking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketId { get; set; }

      
        public int NumberOfTicket { get; set; }

        [Required]
        public string CustomerName { get; set; }

       
        
        public int EvId { get; set; }


        [ForeignKey("EvId")]
        public Event Event { get; set; }



       
    }
}
