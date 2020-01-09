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
        // создаю список из путей к фото
        List<string> ListPathFoto = new List<string>();


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
            //с заголовками (это касается Tile-ов и их скачки).
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

            //============ В ы б о р   к а р т ======================================================
            //Указываем что будем использовать карты Google.
            //gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //Указываем что будем использовать BingMap – Карты от Microsoft
            //gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.BingMap;

            // спутниковы вид карты
            // gMapControl1.MapProvider =GMap.NET.MapProviders.GMapProviders.GoogleSatelliteMap;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.BingSatelliteMap;
            //========================================================================================

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
                MessageBox.Show("У фотографии " + "\"" + Path.GetFileName(imgFilePath) + "\"" + " отсутствуют координаты геолокации", "Предупреждение");
            }
        }

        /* private void OpenToolStripMenuItem_Click_2(object sender, EventArgs e)
         {
             // подчистил элемент listView1
             listView1.Items.Clear();

             //подчистил список фоток
             imageList1.Images.Clear();

             //подчистил маркеры на карте
             gMapControl1.Overlays.Clear();


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

                 listView1.SmallImageList = imageList1;

                 for (int i = 0; i < imageList1.Images.Count; i++)
                 {
                     // в следующей строке {"", означает, что можно в тот же столбец, где и изображение запихать еще и тест
                     // т.е. если убрать "", то название файла будет писаться в столбец "Фото"
                     ListViewItem item = new ListViewItem(new string[] { "", Path.GetFileName(ofd.FileNames[i].ToString()) });
                     item.ImageIndex = i;
                     listView1.Items.Add(item);
                     // добавил строку для того, чтобы после загрузки фоток, камера над картой перемещалась в нужный район
                     //MarkPosition(GetLocation(), Path.GetFileName(ofd.FileNames[i]));
                    // gMapControl1.Position = new PointLatLng(GetLocation())


                 }
             }
         }*/

        //================== метод, который тупо определяет координаты ==========================
        private GeoLocation GetLocation(int i)
        {
            // ==============================================================
            // Надо узнать, как эту строчку писать правильно!!!! на мой взгдяд, не должно быть привязки к конкретному listView1
            //var ind = listView1.SelectedIndices[0];

            var gps = ImageMetadataReader.ReadMetadata(ofd.FileNames[i]).OfType<GpsDirectory>().FirstOrDefault();

            var locationMeta = gps.GetGeoLocation();

            return locationMeta;
        }

        private void MarkPosition(GeoLocation getLocation, string fileName)
        {
            double lat = getLocation.Latitude;
            double lon = getLocation.Longitude;

            //Создаем новый список маркеров, с указанием компонента
            //в котором они будут использоваться и названием списка
            GMapOverlay markersOverlay = new GMapOverlay(gMapControl1, "marker");

            //Инициализация нового ЗЕЛЕНОГО маркера, с указанием его координат
            GMapMarkerGoogleGreen marker = new GMapMarkerGoogleGreen(new PointLatLng(lat, lon));

            marker.ToolTip = new GMapRoundedToolTip(marker);

            //Текст отображаемый при наведении на маркер
            marker.ToolTipText = fileName;

            //Добавляем маркер в список маркеров
            markersOverlay.Markers.Add(marker);

            //Добавляем в компонент, список маркеров
            gMapControl1.Overlays.Add(markersOverlay);

            // Смещаем камеру карты к снимку
            gMapControl1.Position = new PointLatLng(lat, lon);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            // узнаю индекс выделенной строки
            // если выделено несколько строк, то дает индекс последней выделенной строки

            //не понимаю как это работает, но работант!!!------------------------------------------------------------- 
            //var ind = listView1.SelectedIndices[0];
            //ShowLocationFromImgFile(ofd.FileNames[ind]);




        }

        private void listView1_MouseEnter(object sender, EventArgs e)
        {
            listView1.Focus();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int i in listView1.SelectedIndices)
            {
                ShowLocationFromImgFile(ofd.FileNames[i]);
                // показываю на карте где была сфотографирована фотка
                MarkPosition(GetLocation(i), Path.GetFileName(ofd.FileNames[i]));
            }
        }

        private void toolStripButton_ClearMap_Click(object sender, EventArgs e)
        {
            gMapControl1.Overlays.Clear();
            //listView1.SelectedItems = false;
            gMapControl1.ReloadMap();
        }

        private void toolStripButton_OpenFiles_Click(object sender, EventArgs e)
        {
            // подчистил элемент listView1
            listView1.Items.Clear();

            //подчистил список фоток
            imageList1.Images.Clear();

            //подчистил маркеры на карте
            gMapControl1.Overlays.Clear();

            //почистил пути файлов в список PathFoto
            ListPathFoto.Clear();

            ofd.Multiselect = true;
            ofd.Filter = "Файлы изображений (*.jpg, )|*.jpg";
            ofd.Title = "Выберите файлы изображений";

            if (ofd.ShowDialog() == DialogResult.OK)
            //return;
            {
                // пути файлов в список PathFoto
                ListPathFoto.Clear();
                foreach (string f in ofd.FileNames)
                {
                    ListPathFoto.Add(f);
                }

                imageList1.ImageSize = new Size(100, 100);


                foreach (var oneFilePath in ListPathFoto)
                {
                    var image = Image.FromFile(oneFilePath);
                    Image thumb = image.GetThumbnailImage(100, 100, () => false, IntPtr.Zero);
                    imageList1.Images.Add(thumb);
                    image.Dispose();
                }

                listView1.SmallImageList = imageList1;

                for (int j = 0; j < imageList1.Images.Count; j++)
                {
                    // в следующей строке {"", означает, что можно в тот же столбец, где и изображение запихать еще и тест
                    // т.е. если убрать "", то название файла будет писаться в столбец "Фото"
                    ListViewItem item = new ListViewItem(new string[] { "", Path.GetFileName(ofd.FileNames[j].ToString()) })
                    {
                        ImageIndex = j
                    };
                    listView1.Items.Add(item);
                    // добавил эти 2 строки для того, чтобы после загрузки фоток, камера над картой перемещалась в нужный район
                    MarkPosition(GetLocation(j), Path.GetFileName(ofd.FileNames[j]));
                    gMapControl1.Position = new PointLatLng(GetLocation(j).Latitude, GetLocation(j).Longitude);
                }
                
                GC.Collect(); // this is magic
            }
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gMapControl1_MouseEnter(object sender, EventArgs e)
        {
            gMapControl1.Focus();
        }
    }
}

