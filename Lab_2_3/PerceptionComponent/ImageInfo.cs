using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PerceptionComponent
{
    public class ImageInfo
    {
        string imageName;
        ConcurrentBag<ObjDescription> objs;

        public ImageInfo()
        {}

        public ImageInfo(string imgName)
        {
            this.imageName = imgName;
            objs = new ConcurrentBag<ObjDescription>();
        }

        public void Add(ObjDescription obj)
        {
            objs.Add(obj);
        }

        public override string ToString()
        {
            string res = imageName + "\n";
            foreach(ObjDescription desc in objs)
            {
                res += desc.ToString();
            }
            return res;
        }

        public string ImageName
        {
            get
            {
                return imageName;
            }
        }

        public ConcurrentBag<ObjDescription> Objs
        {
            get
            {
                return objs;
            }
            set
            {
                objs = value;
            }
        }
    }
}
