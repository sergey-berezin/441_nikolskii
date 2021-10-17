using System;
using System.ComponentModel;

namespace YOLOv4MLNet
{
    public class View
    {
        readonly Model model;
        readonly int numOfImages;

        int currentNumOfImages;

        public View(int numOfImages)
        {
            this.numOfImages = numOfImages;
            currentNumOfImages = 0;
            model = new Model();
            model.PropertyChanged += Info;
        }

        private void Info(object sender, PropertyChangedEventArgs e)
        {
            ++currentNumOfImages;

            Console.Clear();

            Console.WriteLine("PROGRESS: [" + ((double)currentNumOfImages / numOfImages * 100).ToString() + "%]\n\nCLASSES:");

            foreach (string name in Recognition.classesNames)
            {
                if (model.ClassDict[name] > 0)
                {
                    Console.WriteLine("  " + name + ": " + model.ClassDict[name]);
                }
            }

            Console.WriteLine("\nIMAGES INFO:");

            foreach (ImageInfo image in model.ImgBag)
            {
                Console.WriteLine("  " + image.ToString());
            }
            
        }
        public Model Model
        {
            get
            {
                return model;
            }
        }
    }
}