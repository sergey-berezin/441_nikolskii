using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Immutable;

namespace YOLOv4MLNet
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ImmutableDictionary<string, int> classDict;
        ImmutableDictionary<string, int> classCount;
        ImmutableDictionary<string, ImmutableList<string>> classImages;
        ImmutableList<string> imagesInProcess;

        ConcurrentBag<ImageInfo> imgBag;

        public Model()
        {
            imgBag = new ConcurrentBag<ImageInfo>();
            classCount = ImmutableDictionary<string, int>.Empty;
            classImages = ImmutableDictionary<string, ImmutableList<string>>.Empty;
            imagesInProcess = ImmutableList<string>.Empty;
        }

        public void RemoveImage(int ind)
        {
            lock (imagesInProcess)
            {
                imagesInProcess = imagesInProcess.RemoveAt(ind);
                OnPropertyChanged(nameof(ImagesInProcess));
            }
        }
        public void InsertImage(int idx, string pathToImage)
        {
            lock (imagesInProcess)
            {
                imagesInProcess = imagesInProcess.Insert(idx, pathToImage);
                OnPropertyChanged(nameof(ImagesInProcess));
            }
        }
        public ImmutableDictionary<string, int> ClassCount
        {
            get
            {
                return classDict;
            }
        }
        public ImmutableDictionary<string, ImmutableList<string>> ClassImages
        {
            get
            {
                return classImages;
            }
        }

        public ConcurrentBag<ImageInfo> ImgBag
        {
            get
            {
                return imgBag;
            }
        }

        public void Add(ImageInfo info, string pathToImage)
        {
            lock (imgBag)
            {
                imgBag.Add(info);

                foreach (ObjectInfo item in info.ObjBag)
                {
                    if (classCount.ContainsKey(o.Cls))
                    {
                        classCount = classCount.SetItem(o.Cls, classCount[o.Cls] + 1);
                    }
                    else
                    {
                        classCount = classCount.Add(o.Cls, 1);
                    }

                    if (ClassImages.ContainsKey(o.Cls))
                    {
                        if (!classImages[o.Cls].Contains(pathToImage))
                        {
                            classImages = classImages.SetItem(o.Cls, classImages[o.Cls].Add(pathToImage));
                        }
                    }
                    else
                    {
                        classImages = classImages.Add(o.Cls, (ImmutableList<string>.Empty).Add(pathToImage));
                    }
                }

                OnPropertyChanged(nameof(ClassDict));
            }
        }
        public void AddImage(string pathToImage)
        {
            lock (imagesInProcess)
            {
                imagesInProcess = imagesInProcess.Add(pathToImage);
                OnPropertyChanged(nameof(ImagesInProcess));
            }
        }

        public ImmutableList<string> ImagesInProcess
        {
            get
            {
                return imagesInProcess;
            }
        }
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}