using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS.Models
{
   public class Authority
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FatherId { get; set; }
        public int State { get; set; }
        public string Url { get; set; }
        public int OrderId { get; set; }
    }
}
