using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC_9294_MVC.Models
{
    public class OwnerViewModel
    {
        public int Id { get; set; }

        [DisplayName("OwnerName")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int CarId { get; set; }
    }
}