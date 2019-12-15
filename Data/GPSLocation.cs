using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoPosition.Data
{
    public class GPSLocation
    {
        public string LatitudeRef { get; set; }
        public Coordinats LatitudeCoordinat { get; set; }

        public string LongitudeRef { get; set; }
        public Coordinats LongitudeCoordinat { get; set; }

        public override string ToString()
        {
            var res = $"{LatitudeRef} {LatitudeCoordinat.Deg}° {LatitudeCoordinat.Min}' {LatitudeCoordinat.Sec,2:0.#}\"";
            res += $", {LongitudeRef} {LongitudeCoordinat.Deg}° {LongitudeCoordinat.Min}' {LongitudeCoordinat.Sec,2:0.#}\"";
            return res;
        }
    }
}
