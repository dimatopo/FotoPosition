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

 
            // создаю список из путей к фото
            List<string> ListPathFoto = new List<string>();

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
            return 0;// latitude;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // высота строк
            dataGridView1.RowTemplate.Height = 100;
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

