using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ActionCoreLib.Models;
using ActionCoreLib.Utilities;
using FxDiaryDesktop.Pages;

namespace FxDiaryDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ViewPage pageView = new();
        private static readonly AddDiaryPage pageAddDiary = new();
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(pageView);
            //test
            //MainFrame.Navigate(pageAddDiary);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeMainPage();
        }

        public void ChangePage(Page page)
        {
            MainFrame.Navigate(page);
            backbtn.Visibility = Visibility.Visible;
        }
        public void ChangeMainPage()
        {
            //MainFrame.GoBack();
            MainFrame.RemoveBackEntry();
            MainFrame.Navigate(pageView);
            backbtn.Visibility = Visibility.Collapsed;
        }

        //public void NavigateToViewDetailPage(DiaryModel diary)
        //{
        //    // Chuyển đến trang ViewDetailPage
        //    ViewDetailPage viewDetailPage = new ViewDetailPage(diary);
        //    viewDetailPage.Unloaded += ViewDetailPage_Unloaded; // Đăng ký sự kiện Unloaded
        //    ChangePage(viewDetailPage);
        //}


        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(pageAddDiary);
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            pageView.RefreshUI();
        }
    }

    
}