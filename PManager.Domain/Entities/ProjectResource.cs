using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
