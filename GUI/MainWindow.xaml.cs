using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using Ookii.Dialogs.Wpf;
using System.Windows.Controls;
using PerceptionComponent;
using YOLOv4MLNet;
using System.IO;
using System.Windows.Media.Imaging;

namespace PerceptionGUI
{
    public partial class MainWindow : Window
    {
        WPFView view;
        CancellationTokenSource cts;
        CancellationToken token;
        Perception perc;
        public MainWindow()
        {
            InitializeComponent();
            view = new WPFView();
            perc = new Perception(@"D:\MachineLearning\yolov4.onnx");
            DataContext = view;
            cts = new CancellationTokenSource();
            token = cts.Token;
            ClassCounts.SelectionChanged += ClassCountSelectionChanged;
        }
        private void ChooseFolderButtonClick(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dial = new VistaFolderBrowserDialog();
            bool? res = dial.ShowDialog();
            if (res.HasValue && res.Value)
            {
                FolderPathTextBox.Text = dial.SelectedPath;
            }
        }
        private async void StartPerceptionButtonClick(object sender, RoutedEventArgs e)
        {
            StopPerceptionButton.IsEnabled = true;
            DirectoryInfo di = new DirectoryInfo(FolderPathTextBox.Text);
            IEnumerable<FileInfo> fi = new List<FileInfo>(di.GetFiles());
            var tasks = fi.Select(flinf => perc.StartPerception(view.Model, flinf.FullName, token));
            await Task.WhenAll(tasks).ContinueWith(_ =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    StopPerceptionButton.IsEnabled = false;
                }));
            });
        }
        private void StopPerceptionButtonClick(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
        private void ClassCountSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (ClassCounts.SelectedItem == null)
            {
                ClassImages.ItemsSource = null;
                return;
            }
            string className = ((KeyValuePair<string, int>)ClassCounts.SelectedItem).Key;
            ClassImages.ItemsSource = view.ClassImages[className];
        }
    }
}