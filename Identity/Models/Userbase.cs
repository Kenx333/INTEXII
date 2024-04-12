using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTEXII.Models
{
    public class Userbase
    {
        [Required]
        [Key]
        public int ProductionRecommondation_ID { get; set; }

        [ForeignKey("Product") ]
        public string name { get; set; }

        public string ProductRecm1 { get; set; }
        public string ProductRecm2 { get; set; }
        public string ProductRecm3 { get; set; }
        public string ProductRecm4 { get; set; }
        public string ProductRecm5 { get; set; }

    }
}
