using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTEX2.Models
{
    [PrimaryKey(nameof(transaction_ID), nameof(customer_ID))]
    public class Order
    {
        //this is transaction_id but i am a bit confused...
        public int transaction_ID { get; set; }
        public int customer_ID { get; set; }
        public DateFormat date { get; set; }
        public string day_of_week { get; set; }
        public int time { get; set; }
        public string entry_mode { get; set; }
        public int amount { get; set; }
        public string type_of_transaction { get; set; }
        public string country_of_transaction { get; set; }
        //dont know if we need both ^ and below
        public string shipping_address { get; set; }
        public string bank {  get; set; }
        public string type_of_card { get; set; }
        public bool fraud { get; set; }

    }
}
