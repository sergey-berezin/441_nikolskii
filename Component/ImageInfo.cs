using System.Collections.Concurrent;

namespace YOLOv4MLNet
{
    public class ImageInfo
    {
        readonly string name;
        readonly ConcurrentBag<ObjectInfo> objBag;


        public ImageInfo(string name)
        {
            this.name = name;
            objBag = new ConcurrentBag<ObjectInfo>();
        }

        public ConcurrentBag<ObjectInfo> ObjBag
        {
            get
            {
                return objBag;
            }
        }

        public void Add(ObjectInfo obj)
        {
            objBag.Add(obj);
        }

        public override string ToString()
        {
            string info = "Image: " + name + "\n";
            foreach (ObjectInfo item in objBag)
            {
                info += item.ToString();
            }
            return info;
        }
    }
}