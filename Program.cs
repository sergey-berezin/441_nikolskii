using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using YOLOv4MLNet;

namespace Main
{
    public class Program
    {
        private static void Handler(object sender, ConsoleCancelEventArgs args)
        {
            Environment.Exit(0);
        }

        static async Task Main()
        {
            const string modelPath = @"/Users/nikolskiyvladimir/Desktop/cs_labs/models_cs/yolov4.onnx";
            const string imageFolder = @"/Users/nikolskiyvladimir/Desktop/cs_labs/441_nikolskii/YOLOv4MLNet/Images";

            Recognition recognition = new Recognition(modelPath);

            DirectoryInfo directoryInfo = new DirectoryInfo(imageFolder);
            FileInfo[] images = directoryInfo.GetFiles();

            View view = new View(images.Length);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Console.CancelKeyPress += Handler;
        
            foreach (FileInfo image in images)
            {
                await recognition.StartRecognize(view.Model, image.FullName, cancellationToken);
            }

            Console.WriteLine("DONE.");
        }
    }
}