﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HandsDetection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebcamHandler camera;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            camera = new WebcamHandler(this);
            camera.AddImagesHandler();
            
            camera.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            camera.Stop();
        }

        public void SetTitle(string toTitle)
        {
            Title = toTitle;
        }
    }
}
