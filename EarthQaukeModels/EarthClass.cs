using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EarthQuakeModels
{
     public class EarthClass
     {
        public double? Magnitude { get; set; }
        public int EpicenterDepth { get; set; }
        public string Place { get; set; }
        public DateTime Time { get; set; }
    }
}
