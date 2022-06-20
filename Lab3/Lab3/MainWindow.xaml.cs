using System.Diagnostics;
using System.Net;
using System.Windows;
using Kernel;
using Kernel.DownLoader;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DownloadService _downloadService;

        public MainWindow()
        {
            InitializeComponent();
            var client = new WebClientWrapper();
            var downLoader = new WebsiteDownLoader(client);
            _downloadService = new DownloadService(downLoader);
        }

        public void OnExecuteSyncClicked(object sender, RoutedEventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            ResultBlock.Text = _downloadService.RunDownloadSync();
            var time = watch.Elapsed;
            ResultBlock.Text += $"Total execution time: {time}";
        }

        public async void OnExecuteAsyncClicked(object sender, RoutedEventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            ResultBlock.Text = await _downloadService.RunDownloadAsync();
            var time = watch.Elapsed;
            ResultBlock.Text += $"Total execution time: {time}";
        }

        public async void OnExecuteAsyncParallelClicked(object sender, RoutedEventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            ResultBlock.Text = await _downloadService.RunDownloadAsyncParallel();
            var time = watch.Elapsed;
            ResultBlock.Text += $"Total execution time: {time}";
        }
    }
}
