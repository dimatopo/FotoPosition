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
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms.Markers;




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
            
            //Настройки для компонента GMap.
            gMapControl1.Bearing = 0;

            //CanDragMap - Если параметр установлен в True,
            //пользователь может перетаскивать карту
            ///с помощью правой кнопки мыши.
            gMapControl1.CanDragMap = true;

            //Указываем, что перетаскивание карты осуществляется
            //с использованием левой клавишей мыши.
            //По умолчанию - правая.
            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.GrayScaleMode = true;

            //MarkersEnabled - Если параметр установлен в True,
            //любые маркеры, заданные вручную будет показаны.
            //Если нет, они не появятся.
            gMapControl1.MarkersEnabled = true;

            //Указываем значение максимального приближения.
            gMapControl1.MaxZoom = 21;

            //Указываем значение минимального приближения.
            gMapControl1.MinZoom = 2;

            //Устанавливаем центр приближения/удаления
            //курсор мыши.
            gMapControl1.MouseWheelZoomType =
            GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            //Отказываемся от негативного режима.
            gMapControl1.NegativeMode = false;

            //Разрешаем полигоны.
            gMapControl1.PolygonsEnabled = true;

            //Разрешаем маршруты
            gMapControl1.RoutesEnabled = true;

            //Скрываем внешнюю сетку карты
            //с заголовками.
            gMapControl1.ShowTileGridLines = false;

            //Указываем, что при загрузке карты будет использоваться
            //текущее приближение.
            gMapControl1.Zoom = 18;

            // загрузка определенного места на карте
            gMapControl1.Position = new GMap.NET.PointLatLng(55.032399, 82.913128);


            //Указываем что все края элемента управления
            //закрепляются у краев содержащего его элемента
            //управления(главной формы), а их размеры изменяются
            //соответствующим образом.
            gMapControl1.Dock = DockStyle.Fill;

            //-----Выбор карт------

            //Указываем что будем использовать карты Google.
            //gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //Указываем что будем использовать BingMap – Карты от Microsoft
            //gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.BingMap;

            // спутниковы вид карты
            // gMapControl1.MapProvider =GMap.NET.MapProviders.GMapProviders.GoogleSatelliteMap;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.BingSatelliteMap;

            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            /*
            GMap.NET.MapProviders.GMapProvider.WebProxy =
            System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials =
            System.Net.CredentialCache.DefaultCredentials;
            */



        }
        // тут хочу получить координаты одного выделенного снимка в toolStripStatusLabel1
        private void ShowLocationFromImgFile(string imgFilePath)
        {

            try
            {
                var location = ExtractorLocation.ExtractLocation(imgFilePath);
                toolStripStatusLabel1.Text = location.ToString();
            }
            catch //(Exception ex)
            {
                MessageBox.Show("У фотографии " +Path.GetFileName(imgFilePath)  +  " отсутствуют координаты геолокации", "Предупреждение");
            }


            
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
            var ind = listView1.SelectedIndices[0];
            ShowLocationFromImgFile(ofd.FileNames[ind].ToString());
        }
    }
}

