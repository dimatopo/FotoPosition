using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


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
            Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                //дайт возможность выбора более чем одного фала
                ofd.Multiselect = true;
                ofd.Filter = "Файлы изображений (*.jpg, )|*.jpg";
                ofd.Title = "Выберите файлы изображений";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                // пути файлов в список PathFoto
                ListPathFoto.Clear();
                foreach (string f in ofd.FileNames)
                {
                    ListPathFoto.Add(f);
                }

            }

            imageList1.Images.Clear();
            foreach (var oneFilePath in ListPathFoto)
            {
                var image = Image.FromFile(oneFilePath);
                imageList1.Images.Add(image);
            }

            listView1.Clear();
            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(32, 32);
            listView1.LargeImageList = imageList1;
            for (int j = 0; j < imageList1.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem
                {
                    ImageIndex = j
                };
                listView1.Items.Add(item);
                GC.Collect(); // this is magic
            }

            //dataGridView1.Rows.Clear();
            //foreach (var oneFilePath in ListPathFoto)
            //{
            //    dataGridView1.Rows.Add();
            //    var indexLastRow = dataGridView1.RowCount - 1;

            //    var img = Image.FromFile(oneFilePath);
            //    dataGridView1.Rows[indexLastRow].Cells[0].Value = img;
            //    dataGridView1.Rows[indexLastRow].Cells[1].Value = Path.GetFileName(oneFilePath);

            //    GC.Collect();
            //}

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // высота строк датаГрида
            dataGridView1.RowTemplate.Height = 100;

            // внизу, в тулсрипе....
            toolStripStatusLabel1.Text = "";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            // узнаю индекс выделенной строки
            // если выделено несколько строк, то дает индекс последней выделенной строки
            if (dataGridView1.CurrentRow == null)
                return;

            var ind = dataGridView1.CurrentRow.Index;
            ShowLocationFromImgFile(ListPathFoto[ind]);
        }

        // тут хочу получить координаты одного выделенного снимка в toolStripStatusLabel1
        private void ShowLocationFromImgFile(string imgFilePath)
        {
            var location = ExtractorLocation.ExtractLocation(imgFilePath);
            toolStripStatusLabel1.Text = location.ToString();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var ind =e.ItemIndex;
            ShowLocationFromImgFile(ListPathFoto[ind]);
        }
    }
}

