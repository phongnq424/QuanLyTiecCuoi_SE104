using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

public static class ImageHelper
{
    public static void LoadImageFromRelativePath(Image imageControl, string relativePath)
    {
        try
        {
            if (string.IsNullOrEmpty(relativePath) || imageControl == null)
                return;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = System.IO.Path.Combine(baseDir, relativePath);

            if (File.Exists(fullPath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(fullPath, UriKind.Absolute);
                bitmap.EndInit();

                imageControl.Source = bitmap;
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Không thể hiển thị ảnh: {ex.Message}", "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }
}
