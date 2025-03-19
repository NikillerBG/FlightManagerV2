using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Passenger
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "EGN must be 10 digits.")]
        public string EGN { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string TicketType { get; set; } // "Economy" or "Business"

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
