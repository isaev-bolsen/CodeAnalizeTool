using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageFromWPF
{
    public class ImageFromWPF
    {
        private Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog() { DefaultExt = ".png", Filter = "Png Images (.png)|*.png" };

        private FrameworkElement element;

        public ImageFromWPF(FrameworkElement element)
        {
            this.element = element;
        }

        public void SaveImage(object sender, RoutedEventArgs e)
        {
            if (dlg.ShowDialog().GetValueOrDefault(false))
                try
                {
                    RenderTargetBitmap Bitmap = new RenderTargetBitmap((int)element.ActualWidth * 4, (int)element.ActualHeight * 4, 386, 386, PixelFormats.Pbgra32);
                    Bitmap.Render(element);
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Interlace = PngInterlaceOption.On;
                    encoder.Frames.Add(BitmapFrame.Create(Bitmap));
                    var stream = new FileStream(dlg.FileName, FileMode.Create);
                    encoder.Save(stream);
                    stream.Close();
                }
                catch (System.OutOfMemoryException exc)
                {
                    MessageBox.Show(exc.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
