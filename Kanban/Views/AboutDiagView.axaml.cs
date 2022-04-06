using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Kanban.Views
{
    public partial class AboutDiagView : Window
    {
        public AboutDiagView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.FindControl<Button>("OkButton").Click += async delegate
            {
                Close();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
