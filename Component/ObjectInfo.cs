namespace YOLOv4MLNet
{
    public class ObjectInfo
    {
        private readonly double x1, y1, x2, y2;
        private readonly string name;

        public ObjectInfo(double x1, double y1, double x2, double y2, string name)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public override string ToString()
        {
            return "    Class: " + name + "\n" +
                "    Rectangle diag: " +
                "D1(" + x1.ToString() + "; " + y1.ToString() + ") " +
                "D2(" + x2.ToString() + "; " + y2.ToString() + ")\n";
        }
    }
}