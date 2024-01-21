using ActionCoreLib.Models;
using ActionCoreLib.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FxDiaryDesktop.Pages
{
    /// <summary>
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        private MainWindow? mainWindow => Application.Current.MainWindow as MainWindow;
        private readonly ActionCoreLib.DbEntities.DataContext db = new(GlobalConst.diaryPath);

        private ObservableCollection<ThumbnailModel>? _thumbItems;
        public ObservableCollection<ThumbnailModel> ThumbItems
        {
            get
            {
                return _thumbItems!;
            }
            set
            {
                _thumbItems = value;
            }
        }
        public ViewPage()
        {
            InitializeComponent();

            // Add image paths to the collection
            ThumbItems = new ObservableCollection<ThumbnailModel>();

            var thumb = db.Diary.OrderByDescending(x => x.Id).Select(d => new ThumbnailModel
            {
                Id = d.Id,
                Image = d.ImageEntryTF
            }).Take(GlobalConst.limitThumb);

            ThumbItems = new ObservableCollection<ThumbnailModel>(thumb);

            DataContext = ThumbItems;
        }

        public void RefreshUI()
        {
            ThumbItems.Clear();

            var thumb = db.Diary.OrderByDescending(x => x.Id).Select(d => new ThumbnailModel
            {
                Id = d.Id,
                Image = d.ImageEntryTF
            }).Take(GlobalConst.limitThumb);

            ThumbItems = new ObservableCollection<ThumbnailModel>(thumb);

            DataContext = ThumbItems;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox? item = sender as ListBox;

            if (item?.SelectedItem is ThumbnailModel thumbnail)
            {
                DiaryModel? diary = db.Diary.Find(thumbnail.Id);

                mainWindow!.ChangePage(new ViewDetailPage(diary!));
                // call function to display the diary from MainWindow

                // Query database with id
            }

        }
    
    }

    public partial class ByteArrayToImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray)
            {
                using MemoryStream stream = new(byteArray);
                BitmapImage image = new();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
