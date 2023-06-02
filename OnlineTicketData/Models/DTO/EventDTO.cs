using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTicketData.Models.DTO
{
    public class EventDTO
    {
       

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Please enter EventName")]
        public string EventName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string EventDescription { get; set; }

        public DateTime EventDate { get; set; }

         [Column(TypeName = "varchar(60)")]
    
        [Required(ErrorMessage = "Please enter Location"), MaxLength(60)]
        public string EventLocation { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsApproved { get; set; }

    }
}
