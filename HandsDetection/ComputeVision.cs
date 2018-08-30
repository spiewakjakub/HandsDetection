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
                { ImageEnum.Main, ImagesConverter.ToBitmapSource(mainImage.Mat) }
            };


            

            var yccImage = mat.ToImage<Ycc, byte>();
            var avgColor = mainImage.GetAverage();
            Console.WriteLine(yccImage.GetAverage());

            //var threshold = yccImage.ThresholdBinary(new Ycc(80, 135, 86), new Ycc(255,255,255));
            var threshold = mainImage.ThresholdToZero(avgColor);
            CvInvoke.Canny(threshold, threshold, 150,50,3,true);
            

            computedImages.Add(ImageEnum.Computed, ImagesConverter.ToBitmapSource(threshold.Mat));


            return computedImages;
        }
    }
}
