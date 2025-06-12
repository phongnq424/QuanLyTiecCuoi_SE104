<<<<<<< HEAD
﻿using QuanLyTiecCuoi.MVVM.ViewModel;
=======
﻿using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.MVVM.ViewModel;
>>>>>>> main
using System;
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
using System.Windows.Shapes;

namespace QuanLyTiecCuoi.MVVM.View.MainVindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
<<<<<<< HEAD
            DataContext = vm;
=======
            //DataContext = App.AppHost.Services.GetService<MainWindowViewModel>();
>>>>>>> main
        }
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }
    }
}
