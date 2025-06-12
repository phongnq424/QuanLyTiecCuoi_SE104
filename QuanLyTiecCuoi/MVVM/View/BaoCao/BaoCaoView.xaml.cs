<<<<<<< HEAD:QuanLyTiecCuoi/MainWindow.xaml.cs
﻿using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;
using System.Text;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> main:QuanLyTiecCuoi/MVVM/View/BaoCao/BaoCaoView.xaml.cs
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyTiecCuoi;

namespace QuanLyTiecCuoi.MVVM.View.BaoCao
{
    /// <summary>
    /// Interaction logic for BaoCaoPage.xaml
    /// </summary>
    public partial class BaoCaoPage : Page
    {
        public BaoCaoPage(BaoCaoViewModel vm)
        {
            InitializeComponent();
<<<<<<< HEAD:QuanLyTiecCuoi/MainWindow.xaml.cs

            var sanhViewModel = App.AppHost.Services.GetRequiredService<SanhViewModel>();
            var sanhView = new SanhView(sanhViewModel);
            MainFrame.Navigate(sanhView);

=======
            DataContext = vm;
>>>>>>> main:QuanLyTiecCuoi/MVVM/View/BaoCao/BaoCaoView.xaml.cs
        }
    }
}
