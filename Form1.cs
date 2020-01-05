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
        OpenFileDialog ofd = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
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
/*
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
        //<<<<<<< Updated upstream
        #endregion



        private void OpenToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            // подчистил элемент listView1
            listView1.Items.Clear();
            
            //подчистил список фоток
            imageList1.Images.Clear();

            ofd.Multiselect = true;
            ofd.Filter = "Файлы изображений (*.jpg, )|*.jpg";
            ofd.Title = "Выберите файлы изображений";
            
            //подчистил список фоток
            imageList1.Images.Clear();

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
                
                listView1.SmallImageList = imageList1;

                for (int i = 0; i < imageList1.Images.Count; i++)
                {
                    // в следующей строке {"", означает, что можно в тот же столбец, где и изображение запихать еще и тест
                    // т.е. если убрать "", то название файла будет писаться в столбец "Фото"
                    ListViewItem item = new ListViewItem(new string[] { "", Path.GetFileName(ofd.FileNames[i].ToString()) });
                    item.ImageIndex = i;
                    listView1.Items.Add(item);
                }
            }
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e) => this.Close();

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            // узнаю индекс выделенной строки
            // если выделено несколько строк, то дает индекс последней выделенной строки
            var ind = listView1.SelectedItems[0];
            //ShowLocationFromImgFile(imageList1.Images[ind]);
        }
    }
}

