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
using System.Windows.Threading;

namespace QuanLyTiecCuoi.MVVM.View.TuyChinh
{
    /// <summary>
    /// Interaction logic for AutoCloseMessageBox.xaml
    /// </summary>
    public partial class AutoCloseMessageBox : Window
    {
         private readonly DispatcherTimer _timer;

        public AutoCloseMessageBox(string? message, int durationInSeconds = 3)
        {
            InitializeComponent();
            if(message != null)
                MessageText.Text = message;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(durationInSeconds)
            };
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                Close();
            };
        }

        public new void Show()
        {
            _timer.Start();
            base.Show();
        }

        public new void ShowDialog()
        {
            _timer.Start();
            base.ShowDialog();
        }

        public static void Show(string message, int seconds)
        {
            var box = new AutoCloseMessageBox(message, seconds);
            box.ShowDialog();
        }

        public static void ShowWaiting(int seconds)
        {
            var box = new AutoCloseMessageBox(null, seconds);
            box.ShowDialog();
        }
    }
}
