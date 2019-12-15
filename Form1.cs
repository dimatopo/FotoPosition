using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Windows.Media.Imaging;
using FotoPosition.Data;



namespace FotoPosition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            //дайт возможность выбора более чем одного фала
            ofd.Multiselect = true;
            ofd.Filter = "Файлы изображений (*.jpg, )|*.jpg";
            ofd.Title = "Выберите файлы изображений";

            //создаю список для хранения данных из фотографий формата Bmp
            List<Bitmap> listBmpFoto = new List<Bitmap>();

            // создаю список из путей к фото
            List<string> PathFoto = new List<string>();


            //создаю экземпляр класса Foto
            //Foto foto = new Foto()

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear(); // почистим список

                double d = GetLatitude(ofd.FileName.ToString());
                foreach (string f in ofd.FileNames)
                {
                    PathFoto.Add(f);
                }

                // добавляю нужное количество строк
                dataGridView1.Rows.Add(PathFoto.Count-1);

                // dataGridView1[ int.Parse(f.Count), 0].Value = Image.FromFile(ofd.FileName.ToString());
                //for (int i = 0; i < ofd.FileNames.Length; i++)
                //{
                //    dataGridView1.Rows.Add(Image.FromFile(ofd.FileNames[i].ToString()));
                //dataGridView1.Rows[i].Cells[1].Value = ofd.FileName[i].ToString();
                // }


                foreach (var onePhotoFile in PathFoto)
                {
                    var location = ExtractorLocation.ExtractLocation(onePhotoFile);

                }


                // заполняю DataGrid
                for (int i = 0; i < PathFoto.Count; i++)
                {
                 
                    
                    
                    //StreamWriter sw = new StreamWriter()
                    //Image img;
                   // listBmpFoto.Add(img.Save(PathFoto[i].ToString(), System.Drawing.Imaging.ImageFormat.Bmp);

                   // dataGridView1.Rows[i].Cells[0].Value =
                    


                    //заполняю второй столбец именем файла
                    dataGridView1.Rows[i].Cells[1].Value = Path.GetFileName(PathFoto[i].ToString());



            }

            //var folderName = ofd.FileName.ToString();
            /* var photoFiles = Directory.GetFiles(folderName);
             foreach (var onePhotoFile in photoFiles)
             {
                 // вариант №1 из https://gist.github.com/5342/3293802
                 // - чувак за пивом написал, я чуть классами обернул,чтобы было удобно пользоваться
                 var location = ExtractorLocation.ExtractLocation(onePhotoFile);
                 Console.WriteLine($"{onePhotoFile} - {location}");

             }
             */
        }
    }

    private double GetLatitude(string path)
    {
        double latitude;
        /*
        FileStream Foto = File.Open(path, FileMode.Open, FileAccess.Read); // открыли файл по адресу s для чтения
        //FileAttributes attributes = File.GetAttributes(path);

        //"распаковали" снимок и создали объект decoder
        BitmapDecoder decoder = JpegBitmapDecoder.Create(Foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);

        //считали и сохранили метаданные
        BitmapMetadata TmpImgEXIF = (BitmapMetadata)decoder.Frames[0].Metadata.Clone();

        //TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=1}", "N");

        //TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=3}", "E");
        var loc = TmpImgEXIF.Location;

        //var lon = (BitmapMetadata)decoder.Frames[0].Metadata.ToString();

*/

        return 0;// latitude; 

    }

}

class Bitmap
{
    //public Image image;
    public double lat;
    public double lon;
    public string fileName;
    public Bitmap(double latitude, double longitude, string fName)
    {
        //image = img;
        lat = latitude;
        lon = longitude;
        fileName = fName;
    }
}

}

