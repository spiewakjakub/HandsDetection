using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HandsDetection
{
    class WebcamHandler
    {
        private VideoCapture Webcam;


        public WebcamHandler()
        {
            Webcam = new VideoCapture(0);
        }

        public void AddImagesHandler(Dictionary<ImageEnum, Image> windowImages)
        {
            Webcam.ImageGrabbed += delegate
            {
                var matrixFromCamera = new Mat();
                Webcam.Retrieve(matrixFromCamera);

                var computedImages = ComputeVision.Compute(matrixFromCamera);
                computedImages.Keys.ToList().ForEach(key =>
                {
                    var a = windowImages.TryGetValue(key, value: out Image windowImage);
                    if (computedImages.TryGetValue(key, value: out BitmapSource computedImage) && a)
                    {
                        Application.Current.Dispatcher.Invoke(
                            delegate
                            {
                                windowImage.Source = computedImage;
                                Console.WriteLine(computedImage);
                            });
                    }
                });
            };
        }

        public void Start()
        {
            Webcam.Start();
        }

        public void Stop()
        {
            Webcam.Stop();
        }
    }
}
