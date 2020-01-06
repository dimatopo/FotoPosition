using FotoPosition.Data;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FotoPosition
{
    public class ExtractorLocation
    {
        public static GPSLocation ExtractLocation(string file)
        {
            if (file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("jpeg"))
            {
                Image image = null;
                try
                {
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    image = Image.FromStream(fs);


                    // GPS Tag Names
                    // http://www.sno.phy.queensu.ca/~phil/exiftool/TagNames/GPS.html

                    // Check to see if we have gps data
                    if (Array.IndexOf<int>(image.PropertyIdList, 1) != -1 &&
                        Array.IndexOf<int>(image.PropertyIdList, 2) != -1 &&
                        Array.IndexOf<int>(image.PropertyIdList, 3) != -1 &&
                        Array.IndexOf<int>(image.PropertyIdList, 4) != -1)
                    {
                        var location = new GPSLocation();

                        location.LatitudeRef = BitConverter.ToChar(image.GetPropertyItem(1).Value, 0).ToString();
                        location.LatitudeCoordinat = DecodeRational64u(image.GetPropertyItem(2));
                        location.LongitudeRef = BitConverter.ToChar(image.GetPropertyItem(3).Value, 0).ToString();
                        location.LongitudeCoordinat = DecodeRational64u(image.GetPropertyItem(4));
                        // Console.WriteLine("{0}\t{1} {2}, {3} {4}", file, gpsLatitudeRef, latitude, gpsLongitudeRef, longitude);
                        return location;
                    }
                    //fs.Close();
                }
                finally
                {
                    if (image != null) image.Dispose();
                }
            }
            throw new BadImageFormatException("There is no gps data", file);
            //throw new ArgumentException("File must be .jpg or .jpeg", "file");  было написано это
        }

        private static Coordinats DecodeRational64u(PropertyItem propertyItem)
        {
            uint dN = BitConverter.ToUInt32(propertyItem.Value, 0);
            uint dD = BitConverter.ToUInt32(propertyItem.Value, 4);
            uint mN = BitConverter.ToUInt32(propertyItem.Value, 8);
            uint mD = BitConverter.ToUInt32(propertyItem.Value, 12);
            uint sN = BitConverter.ToUInt32(propertyItem.Value, 16);
            uint sD = BitConverter.ToUInt32(propertyItem.Value, 20);

            decimal deg;
            decimal min;
            decimal sec;
            // Found some examples where you could get a zero denominator and no one likes to devide by zero
            if (dD > 0) { deg = (decimal)dN / dD; } else { deg = dN; }
            if (mD > 0) { min = (decimal)mN / mD; } else { min = mN; }
            if (sD > 0) { sec = (decimal)sN / sD; } else { sec = sN; }

            return new Coordinats { Deg = deg, Min = min, Sec = sec };

            //if (sec == 0) return string.Format("{0}° {1:0.###}'", deg, min);
            //else return string.Format("{0}° {1:0}' {2:0.#}\"", deg, min, sec);
        }
    }
}
