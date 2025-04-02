using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorsClient.Models
{
    public class ColorModel
    {
        public int Type { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public string HexColor => $"#{Red:X2}{Green:X2}{Blue:X2}";
    }
}
