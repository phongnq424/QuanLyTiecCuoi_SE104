﻿using QuanLyTiecCuoi.MVVM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyTiecCuoi.MVVM.View.NhanVien
{
    /// <summary>
    /// Interaction logic for NhanVienPage.xaml
    /// </summary>
    public partial class NhanVienPage : Page
    {
        public NhanVienPage(NhanVienViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
