using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTicketData.Models.DTO
{
    public class TicketBookingDTO
    {
        //[Required]
        //public int TicketId { get; set; }

        [Required]
        public int EvId { get; set; }

        public int NumberOfTicket { get; set; }

        [Required]
        public string CustomerName { get; set; }






    }
}
