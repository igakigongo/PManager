using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Entities
{
    public class Laptop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string LaptopName { get; set; }

        [Required]
        public double Cost { get; set; }
    }

    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public string NumberPlate { get; set; }

        [Required]
        public string CarType { get; set; }
    }

    public class OfficeSpace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public int FloorNumber { get; set; }

        [Required]
        public string RoomNumber { get; set; }
    }
}
