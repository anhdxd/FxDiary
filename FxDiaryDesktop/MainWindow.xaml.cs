using System.Collections.ObjectModel;
using System.ComponentModel;
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
        //private static readonly ViewDetailPage pageDetailDiary = new();
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = pageView;

            try
            {
                UtilitiesFunction.BackupDatabase(GlobalConst.diaryPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Không thể sao lưu dữ liệu:" + e.Message);
            }

            MainFrame.Navigated += frame_Navigated;



        }
        void frame_Navigated(object sender, NavigationEventArgs e)
        {
            MainFrame.NavigationService.RemoveBackEntry();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeMainPage();
        }

        public void ChangePage(Page page)
        {
            MainFrame.Navigate(page);
            //MainFrame.Content = page;
            refreshBtn.Visibility = Visibility.Collapsed;
            backbtn.Visibility = Visibility.Visible;
        }
        public void ChangeMainPage()
        {
            MainFrame.Refresh();
            MainFrame.Navigate(pageView);

            //MainFrame.Content = pageView;
            refreshBtn.Visibility = Visibility.Visible;
            //backbtn.Visibility = Visibility.Collapsed;
        }

        public void ChangeDetailPage(DiaryModel diary)
        {
            //ViewDetailPage pageDetailDiary = new();
            ViewDetailPage pageDetailDiary = new();

            pageDetailDiary?.ChangeData(diary!);
            MainFrame.Navigate(pageDetailDiary);
            //
            
            backbtn.Visibility = Visibility.Visible;
            refreshBtn.Visibility = Visibility.Collapsed;

        }


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