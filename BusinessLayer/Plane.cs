using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Plane
    {
        public int Id { get; set; }

        public PlaneType PlaneType { get; set; }

        [Required]
        public int EconomyCapacity { get; set; }

        [Required]
        public int BusinessCapacity { get; set; }
    }
}
