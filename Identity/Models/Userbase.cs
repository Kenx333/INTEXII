using System.ComponentModel.DataAnnotations;

namespace INTEXII.Models
{
    public class Userbase
    {
        [Required]
        public int ProductionRecommondation_ID { get; set; }
        public string name { get; set; }

        public string ProductRecm1 { get; set; }
        public string ProductRecm2 { get; set; }
        public string ProductRecm3 { get; set; }
        public string ProductRecm4 { get; set; }
        public string ProductRecm5 { get; set; }

    }
}
