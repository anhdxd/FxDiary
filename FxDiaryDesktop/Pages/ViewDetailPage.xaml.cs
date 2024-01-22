using ActionCoreLib.Models;
using System;
using System.Collections.Generic;
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
using System.IO;
using System.Collections.ObjectModel;

namespace FxDiaryDesktop.Pages
{
    /// <summary>
    /// Interaction logic for ViewDetailPage.xaml
    /// </summary>
    public partial class ViewDetailPage : Page
    {
        private readonly byte[] imageDefault = File.ReadAllBytes("Images/default_image.png");
        private DiaryModel _diaryDetail = new();
        public DiaryModel DiaryDetail
        {
            get
            {
                return _diaryDetail;
            }
            set
            {
                _diaryDetail = value;
            }
        }
        public ViewDetailPage(/*DiaryModel diaryDetail*/)
        {
            InitializeComponent();
        }

        public void ChangeData(DiaryModel diaryDetail)
        {
            _diaryDetail = diaryDetail;
            if (_diaryDetail != null)
            {
                _diaryDetail.Image2 ??= imageDefault;
                _diaryDetail.Image3 ??= imageDefault;
            }
            DataContext = this;

            // reload data

        }
    }
}
