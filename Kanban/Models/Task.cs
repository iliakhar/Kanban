using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Platform;

namespace Kanban.Models
{
    public class Task : INotifyPropertyChanged
    {
        private string header;
        private string text;
        private Bitmap pathToImage;
        public string pathName;
        public string Header
        {
            get => header;
            set
            {
                header = value;
                NotifyPropertyChanged();
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                NotifyPropertyChanged();
            }
        }
        public Bitmap PathToImage
        {
            get => pathToImage;
            set
            {
                pathToImage = value;
                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Task(string header, string text, string pathToImage)
        {
            Header = header;
            Text = text;
            AddPathToImage(pathToImage);
        }

        public void AddPathToImage(string pathToImage)
        {
            try
            {
                PathToImage = new Bitmap(pathToImage);
                pathName = pathToImage;
            }
            catch (Exception ex)
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                pathName = $"avares://Kanban/Assets/avalonia-logo.ico";
                PathToImage = new Bitmap(assets.Open(new Uri(pathName)));
            }
        }
    }


}
