using Emgu.CV;
using Emgu.CV.Structure;
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
        private MainWindow Window;


        public WebcamHandler(MainWindow window)
        {
            Webcam = new VideoCapture(0);
            Window = window;
        }

        public void AddImagesHandler()
        {
            Webcam.ImageGrabbed += delegate
            {
                var matrixFromCamera = new Mat();
                Webcam.Retrieve(matrixFromCamera);
                
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Window.MainImage.Source = ComputeVision.Compute(matrixFromCamera); 
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
