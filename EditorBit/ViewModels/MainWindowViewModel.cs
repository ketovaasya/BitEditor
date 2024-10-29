using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;

namespace EditorBit.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private Bitmap _imageSource;

        public Bitmap ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public ICommand LoadImageCommand => ReactiveCommand.CreateFromTask(LoadImage);

        private async System.Threading.Tasks.Task LoadImage()
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter>
        {
            new FileDialogFilter { Name = "TIFF Files", Extensions = { "tiff", "tif" } }
        }
            };

            // Открываем диалог
            var result = await dialog.ShowAsync((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
            if (result != null && result.Length > 0)
            {
                // Загружаем выбранное изображение в UI-потоке
                ImageSource = new Bitmap(result[0]); // убрал Dispatcher.UIThread.InvokeAsync
            }
        }

    }
}


