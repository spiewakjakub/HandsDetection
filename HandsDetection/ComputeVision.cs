using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace HandsDetection
{
    class ComputeVision
    {
        static public Dictionary<ImageEnum, BitmapSource> Compute(Mat mat)
        {
            var mainImage = mat.ToImage<Gray, byte>();
            //mainImage.SetRandNormal(new MCvScalar(64), new MCvScalar(90));

            var computedImages = new Dictionary<ImageEnum, BitmapSource>
            {
                { ImageEnum.Main, ImagesConverter.ToBitmapSource(mat) }
            };

            var avgColor = mainImage.GetAverage();
            //Console.WriteLine(yccImage.GetAverage());

            //var cuda = new Emgu.CV.Cuda.CudaBackgroundSubtractorMOG();
            //cuda.Apply(mainImage,mainImage);

            //var threshold = yccImage.ThresholdBinary();
            //var threshold = SkinThreshold(mat);

            CvInvoke.Canny(mainImage, mainImage, avgColor.Intensity, avgColor.Intensity * 0.99, 3, false);

            computedImages.Add(ImageEnum.Computed, ImagesConverter.ToBitmapSource(mainImage.Mat));
            return computedImages;
        }

        private static Image<Bgra, byte> SkinThreshold(Mat mat)
        {
            var returnImage = mat.ToImage<Bgra, byte>();

            var rgb = mat.ToImage<Rgb, byte>();
            var ycc = mat.ToImage<Ycc, byte>();
            var hsv = mat.ToImage<Hsv, byte>();

            double skinPixelSize = 0;
            for (int y = 0; y < mat.Height; y++)
            {
                for (int x = 0; x < mat.Width; x++)
                {
                    var r = rgb.Data[y, x, 0];
                    var g = rgb.Data[y, x, 1];
                    var b = rgb.Data[y, x, 2];

                    var h = hsv.Data[y, x, 0];
                    var s = hsv.Data[y, x, 1];
                    var v = hsv.Data[y, x, 2];

                    var yc = ycc.Data[y, x, 0];
                    var cr = ycc.Data[y, x, 1];
                    var cb = ycc.Data[y, x, 2];

                    if ( 54 <= yc && yc <= 163 &&
                        131 <= cr && cr <= 157 &&
                        110 <= cb && cb <= 135)
                    {
                        skinPixelSize += 1.0;
                        returnImage.Data[y, x, 0] = 0;
                        returnImage.Data[y, x, 1] = 0;
                        returnImage.Data[y, x, 2] = 255;
                        returnImage.Data[y, x, 3] = 255;
                    }
                }
            }
            Console.WriteLine(skinPixelSize);


            return returnImage;
        }

        private static Image<Bgra, byte> FastThresholdToZero(Mat mat, Bgra min, Bgra max)
        {
            var returnImage = mat.ToImage<Bgra, byte>();

            for (int y = 0; y < mat.Height; y++)
            {
                for (int x = 0; x < mat.Width; x++)
                {
                    var b = returnImage.Data[y, x, 0];
                    var g = returnImage.Data[y, x, 1];
                    var r = returnImage.Data[y, x, 2];
                    var a = returnImage.Data[y, x, 3];

                    if (min.Red     > r     &&  r > max.Red   &&
                        min.Green   > g     &&  g > max.Green &&
                        min.Blue    > b     &&  b > max.Blue  &&
                        min.Alpha   > a     &&  a > max.Alpha )
                    {
                        returnImage.Data[y, x, 0] = 255;
                        returnImage.Data[y, x, 1] = 255;
                        returnImage.Data[y, x, 2] = 255;
                        returnImage.Data[y, x, 3] = 255;
                    }
                }
            }

                    return returnImage;
        }
    }
}
