using ActionCoreLib.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for AddDiaryPage.xaml
    /// </summary>
    public partial class AddDiaryPage : Page
    {
        public AddDiaryPage()
        {
            InitializeComponent();


        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Bitmap))
            {
                // Xử lý paste ảnh vào RichTextBox ở đây
                PasteImageFromClipboard(sender);
                e.CancelCommand(); // Hủy lệnh paste mặc định
            }
        }

        private void PasteImageFromClipboard(Object obj)
        {
            RichTextBox? richTextBox = obj as RichTextBox;

            if (Clipboard.ContainsImage() && richTextBox != null)
            {
                var bitmapSource = Clipboard.GetImage();
                var image = new System.Windows.Controls.Image()
                {
                    Source = bitmapSource,
                    Stretch = Stretch.Uniform,
                };

                var container = new InlineUIContainer(image);
                var paragraph = new Paragraph(container);

                richTextBox.Document.Blocks.Clear();
                richTextBox.Document.Blocks.Add(paragraph);
            }
        }
        private void RichTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool isCtrlV = Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.V;
            if ((e.Key != Key.Delete && e.Key != Key.Back) && !isCtrlV)
            {
                e.Handled = true; // Chặn sự kiện phím nhấn nếu không phải là phím Ctrl+V (Paste)
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                //get image from richtextbox1
                Image? image1 = GetFirstImageFromRichTextBox(rtb1);
                Image? image2 = GetFirstImageFromRichTextBox(rtb2);
                Image? image3 = GetFirstImageFromRichTextBox(rtb3);

                if (image1 == null)
                {
                    MessageBox.Show("Image1 do not null");
                    return;
                }

                // convert Image to bytes and save to Diary 
                byte[] byteImg1 = ConvertImageToByteArray(image1!)!;
                byte[] byteImg2 = ConvertImageToByteArray(image2!)!;
                byte[] byteImg3 = ConvertImageToByteArray(image3!)!;

                // parse data of textbox
                int order = (cmbOrderType.SelectedValue as ComboBoxItem)!.Content.ToString() == "Buy" ? 0 : 1;
                string note = txtNote.Text;
                int riskReward = int.Parse(txtRiskReward.Text);
                var pair = (cbPair.SelectedItem as ComboBoxItem)!.Content.ToString();

                ActionCoreLib.DbEntities.DataContext db = new(GlobalConst.diaryPath);
                ActionCoreLib.Models.DiaryModel diary = new()
                {
                    Note = note,
                    Pair = pair,
                    OrderType = order,
                    RiskReward = riskReward,
                    TimeStamp = UtilitiesFunction.GetUnixTimestampInSeconds(),

                    ImageEntryTF = byteImg1,
                    Image2 = byteImg2,
                    Image3 = byteImg3,
                };

                db.Diary.Add(diary);
                db.SaveChanges();

                MessageBox.Show("Save success");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Exception btnSave_Click:{ex.Message}");
                return;
            }

        }

        private Image? GetFirstImageFromRichTextBox(RichTextBox richTextBox)
        {
            foreach (Block block in richTextBox.Document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is InlineUIContainer container && container.Child is Image image)
                        {
                            return image;
                        }
                    }
                }
            }

            return null; // Trả về null nếu không tìm thấy hình ảnh
        }

        private byte[]? ConvertImageToByteArray(Image image)
        {
            try
            {
                byte[]? byteArray = null;
                if (image == null)
                {
                    return null;
                }
                // Tạo một MemoryStream và ghi Image vào đó
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder(); // Chọn định dạng hình ảnh mong muốn (ví dụ: PNG)
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
                    encoder.Save(memoryStream);

                    byteArray = memoryStream.ToArray();
                }

                return byteArray;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception ConvertImageToByteArray:{e.Message}");
                return null;
            }


        }

    }

}
