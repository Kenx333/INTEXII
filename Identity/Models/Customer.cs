using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int customer_ID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set;}

        public DateOnly birth_date { get; set; }
        public string country_of_residence { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
    }
}
