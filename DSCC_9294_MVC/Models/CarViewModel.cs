using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC_9294_MVC.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [DisplayName("CarName")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public bool IsUpgraded { get; set; }
    }
}