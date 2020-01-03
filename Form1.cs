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
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;


namespace FotoPosition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // создаю список из путей к фото
        List<string> ListPathFoto = new List<string>();
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


            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear(); // почистим список

                // пути файлов в список PathFoto
                foreach (string f in ofd.FileNames)
                {
                    ListPathFoto.Add(f);
                }


                // заполняю DataGrid
                for (int i = 0; i < ListPathFoto.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.RowTemplate.Height = 100;

                    var img = new Bitmap(ListPathFoto[i].ToString());

                    dataGridView1.Rows[i].Cells[0].Value = img;
                    //col_Picture.Image = Image.FromFile();

                    //заполняю второй столбец именем файла
                    dataGridView1.Rows[i].Cells[1].Value = Path.GetFileName(ListPathFoto[i].ToString());

                }


                /* var gps = ImageMetadataReader.ReadMetadata(ListPathFoto[1])
                              .OfType<GpsDirectory>()
                              .FirstOrDefault();
                 var locationMeta = gps.GetGeoLocation();
                 MessageBox.Show(locationMeta.ToString());*/

                var folderName = ofd.FileName.ToString();
                var photoFiles = System.IO.Directory.GetFiles(Path.GetDirectoryName(folderName));
                foreach (var onePhotoFile in photoFiles)
                {
                    // вариант №1 из https://gist.github.com/5342/3293802
                    // - чувак за пивом написал, я чуть классами обернул,чтобы было удобно пользоваться
                    var location = ExtractorLocation.ExtractLocation(onePhotoFile);
                    Console.WriteLine($"{onePhotoFile} - {location}");
                    Console.WriteLine();
                }

            }
        }

        private double GetLatitude(string path)
        {
            double latitude;
            return 0;// latitude;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // высота строк датаГрида
            dataGridView1.RowTemplate.Height = 100;

            // внизу, в тулсрипе....
            toolStripStatusLabel1.Text = "Широта 0, Долгота 0";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            // узнаю индекс выделенной строки
            // если выделено несколько строк, то дает индекс последней выделенной строки
            var ind = dataGridView1.CurrentRow.Index;
            GetLocationInToolstrip(ind);
        }

        // тут хочу получить координаты одного выделенного снимка в toolStripStatusLabel1
        private void GetLocationInToolstrip(int i)
        {
            var location = ExtractorLocation.ExtractLocation(ListPathFoto[i]);
            toolStripStatusLabel1.Text = location.ToString();
        }

    }

    /*class Bitmap
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
    */
}

