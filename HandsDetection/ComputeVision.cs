using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HandsDetection
{
    class ComputeVision
    {
        static public Dictionary<ImageEnum, BitmapSource> Compute(Mat mat)
        {
            var ret = new Dictionary<ImageEnum, BitmapSource>
            {
                { ImageEnum.Main, ImagesConverter.ToBitmapSource(mat.ToImage<Bgra, byte>().Mat) }
            };
            return new Dictionary<ImageEnum, BitmapSource>(ret);
        }
    }
}
