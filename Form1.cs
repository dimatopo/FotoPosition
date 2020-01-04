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
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Файлы изображений (*.jpg, )|*.jpg";
            ofd.Title = "Выберите файлы изображений";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // запихнул все фотки в imageList1
                foreach (string f in ofd.FileNames)
                {
                    try
                    {
                        imageList1.Images.Add(Image.FromFile(f));
                    }
                    catch
                    {
                        Console.WriteLine("Что-то пошло не так...!");
                    }
                }

                listView1.View = View.LargeIcon;
                listView1.LargeImageList = imageList1;

                for (int i = 0; i < imageList1.Images.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    listView1.Items.Add(item);
                    //listView1.Item
                }

            }
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // внизу, в тулсрипе....
            toolStripStatusLabel1.Text = "";
        }
        // тут хочу получить координаты одного выделенного снимка в toolStripStatusLabel1
        private void ShowLocationFromImgFile(string imgFilePath)
        {
            var location = ExtractorLocation.ExtractLocation(imgFilePath);
            toolStripStatusLabel1.Text = location.ToString();
        }

        #region
        /*
        // создаю список из путей к фото
        List<string> ListPathFoto = new List<string>();
       

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
                foreach (var oneFilePath in ListPathFoto)
                {
                    dataGridView1.Rows.Add();
                    var indexLastRow = dataGridView1.RowCount - 1;
                    dataGridView1.RowTemplate.Height = 100;

                    var img = new Bitmap(oneFilePath);
                    dataGridView1.Rows[indexLastRow].Cells[0].Value = img;
                    dataGridView1.Rows[indexLastRow].Cells[1].Value = Path.GetFileName(oneFilePath);
                }


            }
        }

       

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            // узнаю индекс выделенной строки
            // если выделено несколько строк, то дает индекс последней выделенной строки
            var ind = dataGridView1.CurrentRow.Index;
            ShowLocationFromImgFile(ListPathFoto[ind]);
        }
        */
        #endregion



    }
}

