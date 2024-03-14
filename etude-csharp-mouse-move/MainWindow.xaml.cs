using System.Runtime.InteropServices;
using System.Windows;

namespace etude_csharp_mouse_move
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
#pragma warning disable SYSLIB1054 // コンパイル時に P/Invoke マーシャリング コードを生成するには、'DllImportAttribute' の代わりに 'LibraryImportAttribute' を使用します
        static extern bool SetCursorPos(int x, int y);
#pragma warning restore SYSLIB1054 // コンパイル時に P/Invoke マーシャリング コードを生成するには、'DllImportAttribute' の代わりに 'LibraryImportAttribute' を使用します

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var startX = 100;
            var startY = 100;
            var endX = 500;
            var endY = 500;
            var duration = 1000; // ミリ秒

            var mouseThread = new Thread(() =>
            {
                DateTime startTime = DateTime.Now;

                while (true)
                {
                    var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
                    var t = Math.Min(elapsed / duration, 1.0);

                    var currentX = (int)Math.Round(startX + (endX - startX) * t);
                    var currentY = (int)Math.Round(startY + (endY - startY) * t);

                    SetCursorPos(currentX, currentY);

                    if (t >= 1.0) break;

                    Thread.Sleep(10); // 10ミリ秒ごとに更新
                }
            });

            mouseThread.Start();
        }
    }
}