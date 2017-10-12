using System;
using System.Drawing;

namespace FatCounter
{
    class Program
    {
        static void Main()
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create("http://www.gallowayfamilybutchers.co.uk/wp-content/uploads/raw-products-renamed/ribeye-steaks.jpg");
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            if (responseStream == null) return;

            Bitmap pic = new Bitmap(responseStream);

            int whiteCount = 0;
            int redCount = 0;

            var whiteThreshold = Color.LightGray; //-2894893
            var minRedThreshold = Color.PaleVioletRed; //-2396013
            var maxRedThreshold = Color.DarkRed; //-7667712

            for (int x = 0; x < pic.Width; x++)
            {
                for (int y = 0; y < pic.Height; y++)
                {
                    var pixel = pic.GetPixel(x, y);

                    //if (pixel.ToArgb() > whiteThreshold.ToArgb())
                    //{
                    //    whiteCount++;
                    //}

                    var yy = 0.2126 * pixel.R + 0.7152 * pixel.G + 0.0722 * pixel.B; //luminance

                    if (yy > 128)
                    {
                        whiteCount++;
                    }

                    //if (pixel.R > 220 && pixel.G > 220 && pixel.B > 220)
                    //{
                    //    whiteCount++;
                    //}

                    //if (pic.GetPixel(x, y).ToArgb() > maxRedThreshold.ToArgb() && pic.GetPixel(x, y).ToArgb() > minRedThreshold.ToArgb())
                    //{
                    //    redCount++;
                    //}

                    if (pixel.R > pixel.B && pixel.R > pixel.G && pixel.G <= pixel.B)
                    {
                        redCount++;
                    }
                }
            }

            var whitePercentage = decimal.Divide(whiteCount, whiteCount+redCount) * 100;
            var whitePercentage2 = decimal.Divide(whiteCount, pic.Height * pic.Width) * 100;
            var redPercentage = decimal.Divide(redCount, pic.Height*pic.Width) * 100;

            Console.Write("{0}%", whitePercentage);
            Console.ReadKey();
        }
    }
}
