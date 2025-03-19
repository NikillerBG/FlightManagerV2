using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        public Country From { get; set; }

        [Required]
        public Country To { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public Plane Plane { get; set; }

        [Required]
        public string PilotName { get; set; }
    }
}
