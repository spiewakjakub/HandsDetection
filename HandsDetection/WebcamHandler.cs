using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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

                Application.Current.Dispatcher.Invoke(delegate
                {
                    var computedImages = ComputeVision.Compute(matrixFromCamera);
                    computedImages.Keys.ToList().ForEach(key => {
                        if (computedImages.TryGetValue(key, out var computedImage) && windowImages.TryGetValue(key, out var windowImage))
                        {
                            windowImage.Source = computedImage;
                        }
                    });
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
