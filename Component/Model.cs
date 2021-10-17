using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YOLOv4MLNet
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly ConcurrentDictionary<string, int> classDict;
        readonly ConcurrentBag<ImageInfo> imgBag;

        public Model()
        {
            imgBag = new ConcurrentBag<ImageInfo>();
            classDict = new ConcurrentDictionary<string, int>();

            for (int i = 0; i < Recognition.classesNames.Length; ++i)
            {
                classDict.TryAdd(Recognition.classesNames[i], 0);
            }
        }

        public ConcurrentDictionary<string, int> ClassDict
        {
            get
            {
                return classDict;
            }
        }

        public ConcurrentBag<ImageInfo> ImgBag
        {
            get
            {
                return imgBag;
            }
        }

        public void Add(ImageInfo info)
        {
            lock (imgBag)
            {
                imgBag.Add(info);

                foreach (ObjectInfo item in info.ObjBag)
                {
                    ++classDict[item.Name];
                }

                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}