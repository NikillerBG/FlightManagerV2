using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int FlightId { get; set; }

        public Flight Flight { get; set; }

        public int PassengerId { get; set; }

        public Passenger Passenger { get; set; }
    }
}
