using System.Collections.Immutable;
using System.ComponentModel;

namespace PerceptionComponent
{
    public class WPF : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Model model = new Model();
        public WPF()
        {
            model = new Model();
            PropertyChanged += q;
            model.PropertyChanged += (s, e) =>
            {
                NotifyVM(e.PropertyName);
            };
        }
        private void NotifyVM(string name)
        {
            OnPropertyChanged(name);
        }
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void q(object sender, PropertyChangedEventArgs e)
        {
            return;
        }
        public ImmutableDictionary<string, int> ClassCount => model.ClassCount;
        public ImmutableDictionary<string, ImmutableList<string>> ClassImages => model.ClassImages;
        public ImmutableList<string> ImagesInProcess => model.ImagesInProcess;
        public Model Model
        {
            get
            {
                return model;
            }
        }
    }
}