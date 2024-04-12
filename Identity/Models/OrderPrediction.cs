using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace INTEXII.Models
{
    public class OrderPrediction
    {
        public Order Orders { get; set; }
        public string Prediction { get; set; }
    }
}
