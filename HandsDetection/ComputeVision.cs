using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HandsDetection
{
    class ComputeVision
    {
        static CascadeClassifier cascadeClassifier = new CascadeClassifier("C:\\Users\\jakub\\opencv\\haarcascade\\haarcascades\\palm2.xml");
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        private void SetPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        static public BitmapSource Compute(Mat mat)
        {
            var colourful = mat.ToImage<Bgr, byte>();
            var gray = mat.ToImage<Gray, byte>();
            var decetded = cascadeClassifier.DetectMultiScale(gray);

            //foreach (var rect in decetded)
            //{
            var rects = new List<Rectangle>(decetded);
            if (rects.Capacity == 0)
                return ImagesConverter.ToBitmapSource(mat);
            rects.Sort((rect1, rect2) => rect1.Height.CompareTo(rect2.Height)); 
            var rect = rects[0];
            SetCursorPos(
                    (int)((rect.X / Application.Current.MainWindow.ActualWidth) * System.Windows.SystemParameters.PrimaryScreenWidth), 
                    (int)((rect.Y / Application.Current.MainWindow.ActualHeight) * System.Windows.SystemParameters.PrimaryScreenHeight));
                colourful.Draw(rect, new Bgr(255, 255, 0), 3);
            //}
            
            return ImagesConverter.ToBitmapSource(colourful.Mat);
        }
    }
}
