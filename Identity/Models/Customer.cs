using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace INTEX2.Models
{
    public class Customer
    {
        public int customer_ID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set;}

        //may want to change data type
        public DateFormat birth_date { get; set; }
        public string country_of_residence { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
    }
}
