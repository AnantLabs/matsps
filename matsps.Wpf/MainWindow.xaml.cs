using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using TreeViewwithCheckBoxes;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using Demo.WindowsPresentation.CustomMarkers;
using matsps.Wpf.CustomMarkers;
using System.Windows.Threading;
using matsps.Wpf.ModalWindows;

namespace matsps.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        #region Поля
        PointLatLng start;
        PointLatLng end;

        //internal readonly GMapOverlay routes = new GMapOverlay("routes");

        public ObservableCollection<Node> Nodes { get; private set; }

        // markers
        /// <summary>
        /// current mouse markers
        /// </summary>
        GMapMarker currentMarker;

        // route markers
        GMapMarker routeStart;
        GMapMarker routeEnd;
        GMapMarker routePath;

        /// <summary>
        /// Матрица расстояний по дорогам
        /// </summary>
        private double[,] _matrixRoad = null;        
        #endregion Поля

        #region Конструкторы

        public MainWindow()
        {
            InitializeComponent();

            // set cache mode only if no internet avaible
            try
            {
                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("www.bing.com");
            }
            catch
            {
                MainMap.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET - Demo.WindowsPresentation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // config map
            MainMap.MapProvider = GMapProviders.OpenStreetMap;
            MainMap.Position = new PointLatLng(55.746474, 37.667109); // 37.667109    55.746474
            MainMap.MinZoom = 1;
            MainMap.MaxZoom = 17;
            MainMap.Zoom = 10;

            // map events
            MainMap.OnPositionChanged += new PositionChanged(MainMap_OnCurrentPositionChanged);
            MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
            MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
            MainMap.MouseMove += new System.Windows.Input.MouseEventHandler(MainMap_MouseMove);
            MainMap.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainMap_MouseLeftButtonDown);
            MainMap.Loaded += new RoutedEventHandler(MainMap_Loaded);
            MainMap.MouseEnter += new MouseEventHandler(MainMap_MouseEnter);

            // get map types            
            comboBoxMapType.ItemsSource = GMapProviders.List;
            comboBoxMapType.DisplayMemberPath = "Name";
            comboBoxMapType.SelectedItem = MainMap.MapProvider;

            // acccess mode
            comboBoxMode.ItemsSource = Enum.GetValues(typeof(AccessMode));
            MainMap.Manager.Mode = AccessMode.ServerOnly;
            comboBoxMode.SelectedItem = MainMap.Manager.Mode;



            /*
            // get cache modes
            checkBoxCacheRoute.IsChecked = MainMap.Manager.UseRouteCache;
            checkBoxGeoCache.IsChecked = MainMap.Manager.UseGeocoderCache;

            // setup zoom min/max
            sliderZoom.Maximum = MainMap.MaxZoom;
            sliderZoom.Minimum = MainMap.MinZoom;

            // get position
            textBoxLat.Text = MainMap.Position.Lat.ToString(CultureInfo.InvariantCulture);
            textBoxLng.Text = MainMap.Position.Lng.ToString(CultureInfo.InvariantCulture);

            // get marker state
            checkBoxCurrentMarker.IsChecked = true;

            // can drag map
            checkBoxDragMap.IsChecked = MainMap.CanDragMap;

#if DEBUG
            checkBoxDebug.IsChecked = true;
#endif

            //validator.Window = this;
            */

            // set current marker
            currentMarker = new GMapMarker(MainMap.Position);
            {
                currentMarker.Shape = new CustomMarkerRed(this, currentMarker, "custom position marker");
                currentMarker.Offset = new System.Windows.Point(-15, -15);
                currentMarker.ZIndex = int.MaxValue;
                MainMap.Markers.Add(currentMarker);
            }

            /*
            //if(false)
            {
                // add my city location for demo
                GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;

                PointLatLng? city = GMapProviders.GoogleMap.GetPoint("Russia, Moscow", out status);
                if (city != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                {
                    GMapMarker it = new GMapMarker(city.Value);
                    {
                        it.ZIndex = 55;
                        it.Shape = new CustomMarkerDemo(this, it, "Welcome to Lithuania! ;}");
                    }
                    MainMap.Markers.Add(it);

                    #region -- add some markers and zone around them --
                    {
                        List<PointAndInfo> objects = new List<PointAndInfo>();
                        {
                            string area = "Antakalnis";
                            PointLatLng? pos = GMapProviders.GoogleMap.GetPoint("Lithuania, Vilnius, " + area, out status);
                            if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                            {
                                objects.Add(new PointAndInfo(pos.Value, area));
                            }
                        }
                        {
                            string area = "Senamiestis";
                            PointLatLng? pos = GMapProviders.GoogleMap.GetPoint("Lithuania, Vilnius, " + area, out status);
                            if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                            {
                                objects.Add(new PointAndInfo(pos.Value, area));
                            }
                        }
                        {
                            string area = "Pilaite";
                            PointLatLng? pos = GMapProviders.GoogleMap.GetPoint("Lithuania, Vilnius, " + area, out status);
                            if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
                            {
                                objects.Add(new PointAndInfo(pos.Value, area));
                            }
                        }
                        AddDemoZone(8.8, city.Value, objects);
                    }
                    #endregion
                }
            }
             */

            /*
            // perfromance test
            timer.Interval = TimeSpan.FromMilliseconds(44);
            timer.Tick += new EventHandler(timer_Tick);

            // transport demo
            transport.DoWork += new DoWorkEventHandler(transport_DoWork);
            transport.ProgressChanged += new ProgressChangedEventHandler(transport_ProgressChanged);
            transport.WorkerSupportsCancellation = true;
            transport.WorkerReportsProgress = true;
             */

            //=====================================================
            //      TreeView
            //=====================================================
            Nodes = new ObservableCollection<Node>();
            FillingTree();
        }
        #endregion Конструкторы

        // tile loading stops
        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            System.Windows.Forms.MethodInvoker m = delegate()
            {
                progressBar1.Visibility = Visibility.Hidden;
                //groupBox3.Header = "loading, last in " + MainMap.ElapsedMilliseconds + "ms";
            };

            try
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
            }
            catch
            {
            }
        }

        // tile louading starts
        void MainMap_OnTileLoadStart()
        {
            System.Windows.Forms.MethodInvoker m = delegate()
            {
                progressBar1.Visibility = Visibility.Visible;
            };

            try
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
            }
            catch
            {
            }
        }

        // chenged map type
        void MainMap_OnMapTypeChanged(GMapProvider type)
        {
            // !!! ZOOM SLIDER
            //sliderZoom.Minimum = MainMap.MinZoom;
            //sliderZoom.Maximum = MainMap.MaxZoom;
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point p = e.GetPosition(MainMap);
                currentMarker.Position = MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
            }
        }

        void MainMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(MainMap);
            currentMarker.Position = MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
        }

        // center markers on load
        void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            //MainMap.ZoomAndCenterMarkers(null);
        }

        void MainMap_MouseEnter(object sender, MouseEventArgs e)
        {
            MainMap.Focus();
        }

        /// <summary>
        /// Take Id from CheckBox Uid and transfer value to CheckBoxId struct
        /// </summary>
        /// <param name="sender">The CheckBox clicked.</param>
        /// <param name="e">Parameters associated to the mouse event.</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;
            CheckBoxId.checkBoxId = currentCheckBox.Uid;
        }

        /// <summary>
        /// Filling tree different values
        /// </summary>
        private void FillingTree()
        {
            Nodes.Clear();
            for (int i = 0; i < 5; i++)
            {
                var level_1_items = new Node() { Text = " Level 1 Item " + (i + 1) };
                for (int j = 0; j < 2; j++)
                {
                    var level_2_items = new Node() { Text = " Level 2 Item " + (j + 1) };
                    level_2_items.Parent.Add(level_1_items);
                    level_1_items.Children.Add(level_2_items);
                    for (int n = 0; n < 2; n++)
                    {
                        var level_3_items = new Node() { Text = " Level 3 Item " + (n + 1) };
                        level_3_items.Parent.Add(level_2_items);
                        level_2_items.Children.Add(level_3_items);
                    }
                }

                Nodes.Add(level_1_items);
            }
            treeView.ItemsSource = Nodes;
        }



        // current location changed
        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            mapgroup.Header = "gmap: " + point;
        }

        // reload
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MainMap.ReloadMap();
        }

        // enable current marker
        private void checkBoxCurrentMarker_Checked(object sender, RoutedEventArgs e)
        {
            if (currentMarker != null)
            {
                MainMap.Markers.Add(currentMarker);
            }
        }

        // disable current marker
        private void checkBoxCurrentMarker_Unchecked(object sender, RoutedEventArgs e)
        {
            if (currentMarker != null)
            {
                MainMap.Markers.Remove(currentMarker);
            }
        }

        // enable map dragging
        private void checkBoxDragMap_Checked(object sender, RoutedEventArgs e)
        {
            if (MainMap != null)
                MainMap.CanDragMap = true;
        }

        // disable map dragging
        private void checkBoxDragMap_Unchecked(object sender, RoutedEventArgs e)
        {
            MainMap.CanDragMap = false;
        }

        // change server\cache etc
        private void comboBoxMode_DropDownClosed(object sender, EventArgs e)
        {
            MainMap.Manager.Mode = (AccessMode)comboBoxMode.SelectedItem;
            MainMap.ReloadMap();
        }

        // change map
        private void comboBoxMapType_DropDownClosed(object sender, EventArgs e)
        {
            MainMap.MapProvider = comboBoxMapType.SelectedItem as GMapProvider;
            MainMap.ReloadMap();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageSource img = MainMap.ToImageSource();
                PngBitmapEncoder en = new PngBitmapEncoder();
                en.Frames.Add(BitmapFrame.Create(img as BitmapSource));

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "GMap.NET Image"; // Default file name
                dlg.DefaultExt = ".png"; // Default file extension
                dlg.Filter = "Image (.png)|*.png"; // Filter files by extension
                dlg.AddExtension = true;
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                // Show save file dialog box
                bool? result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    using (System.IO.Stream st = System.IO.File.OpenWrite(filename))
                    {
                        en.Save(st);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // enables tile grid view
        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            MainMap.ShowTileGridLines = true;
        }

        // disables tile grid view
        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            MainMap.ShowTileGridLines = false;
        }

        // sets route start
        private void rbtnStart_Click(object sender, RoutedEventArgs e)
        {
            start = currentMarker.Position;
        }

        // sets route end
        private void rbtnEnd_Click(object sender, RoutedEventArgs e)
        {
            end = currentMarker.Position;
        }

        // adds route
        private void rbtnAddRoute_Click(object sender, RoutedEventArgs e)
        {
            rbtnClearRoute_Click(this, new RoutedEventArgs());

            RoutingProvider rp = MainMap.MapProvider as RoutingProvider;
            if (rp == null)
            {
                rp = GMapProviders.GoogleMap; // use google if provider does not implement routing
            }

            MapRoute route = rp.GetRoute(start, end, false, false, (int)MainMap.Zoom);
            if (route != null)
            {
                routeStart = new GMapMarker(start);
                routeStart.Shape = new CustomMarkerDemo(this, routeStart, "Start: " + start.ToString());

                routeEnd = new GMapMarker(end);
                routeEnd.Shape = new CustomMarkerDemo(this, routeEnd, "End: " + end.ToString());

                routePath = new GMapMarker(start);
                {
                    routePath.Route.AddRange(route.Points);
                    routePath.RegenerateRouteShape(MainMap);
                    routePath.Position = route.Points[0];

                    routePath.ZIndex = -1;
                }
                rlRouteLength.Content = String.Format("length: {0:#.00} km", route.Distance);

                MainMap.Markers.Add(routeStart);
                MainMap.Markers.Add(routeEnd);
                MainMap.Markers.Add(routePath);

                //MainMap.ZoomAndCenterMarkers(null);
            }
        }

        // clear route (not realize)
        private void rbtnClearRoute_Click(object sender, RoutedEventArgs e)
        {
            if (routePath != null)
            {
                routePath.Shape.Visibility = System.Windows.Visibility.Hidden;
                routePath.Clear();
                routePath = null;
            }
            if (routeStart != null)
            {
                routeStart.Shape.Visibility = System.Windows.Visibility.Hidden;
                routeStart.Clear();
                routeStart = null;
            }
            if (routeEnd != null)
            {
                routeEnd.Shape.Visibility = System.Windows.Visibility.Hidden;
                routeEnd.Clear();
                routeEnd = null;
            }
            rlRouteLength.Content = String.Format("length: {0:#.00} km", 0.0);
        }

        #region Расчет маршрута между случайными точками
        private void btnGenerateRandomPoints_Click(object sender, RoutedEventArgs e)        
        {
            matsps.CommonData.CitiesCollection _cities;

            // Создаем Города
            int iCitiesCount;
            try
            {
                iCitiesCount = Convert.ToInt32(txbRandomPointsCount.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка преобразования из текста в целое число: " + ex.Message);
                return;
            }
            _cities = new matsps.CommonData.CitiesCollection(iCitiesCount);
            _cities.MaxDistance = 100;
            _cities.InitCitiesRandom();

            MainMap.Markers.Clear();
            MainMap.Markers.Add(currentMarker);
            RectLatLng viewArea = MainMap.ViewArea;

            // Список макеров
            List<GMapMarker> listMarkers = new List<GMapMarker>();

            // Выводим города на карту
            for (int i = 0; i < iCitiesCount; i++)
            {
                PointLatLng point = new PointLatLng();
                point.Lat = (viewArea.HeightLat / 100.0) * (double)(_cities[i].Y) + viewArea.Bottom;
                point.Lng = (viewArea.WidthLng / 100.0) * (double)(_cities[i].X) + viewArea.Left;
                GMapMarker newMarker = new GMapMarker(point);
                newMarker.Shape = new Test(this, newMarker, _cities[i].Index.ToString()); //CustomMarkerRed(this, newMarker, _cities[i].Index.ToString());
                newMarker.Offset = new System.Windows.Point(-15, -15);
                newMarker.ZIndex = _cities[i].Index;

                MainMap.Markers.Add(newMarker);
                listMarkers.Add(newMarker);
            }

            // Заполняем дерево
            Nodes.Clear();
            Nodes.Add(new Node() { Text = String.Format("random points", iCitiesCount) });
            for (int i = 0; i < iCitiesCount; i++)
            {
                var treeItem = new Node() { Name = "id" + _cities[i].Index, Text = String.Format("point {0}", _cities[i].Index + 1) };

                // binding (does not work)
                Binding binding = new Binding()
                 {
                     ElementName = "id" + _cities[i].Index,
                     Path = new PropertyPath("IsChecked"),
                     Converter = new VisibilityTest.BooleanToVisibilityConverter(),
                     Mode = BindingMode.TwoWay
                 };
                //MainMap.Markers[i + 1].Map.SetBinding(UIElement.VisibilityProperty, binding);

                Nodes[0].Children.Add(treeItem);
            }
            Nodes[0].IsExpanded = true;
            treeView.ItemsSource = Nodes;

            // Расчет матрицы расстояний (по дорогам) //и ( по линиям)
            _matrixRoad = new double[iCitiesCount, iCitiesCount];
            //double[,] matrixLine = new double[iCitiesCount, iCitiesCount];
            for (int i = 0; i < iCitiesCount; i++)
            {
                for (int j = 0; j < iCitiesCount; j++)
                {
                    if (i != j)
                    {
                        RoutingProvider rp = MainMap.MapProvider as RoutingProvider;
                        if (rp == null)
                        {
                            rp = GMapProviders.GoogleMap; // use google if provider does not implement routing
                        }

                        MapRoute route = rp.GetRoute(listMarkers[i].Position, listMarkers[i].Position, false, false, (int)MainMap.Zoom);
                        if (route != null)
                        {
                            _matrixRoad[i, j] = route.Distance;
                            //matrixLine[i, j] = route.DistanceLine;
                        }
                    }
                    else
                    {
                        _matrixRoad[i, j] = double.PositiveInfinity;
                        //matrixLine[i, j] = double.PositiveInfinity;
                    }
                }
            }
        }

        /// <summary>
        /// Открыть таблицу расстояний между точками
        /// </summary>
        private void btnTableDistance_Click(object sender, RoutedEventArgs e)
        {
            wndDistanceTable wind = new wndDistanceTable();
            wind.Matrix = _matrixRoad;
            wind.Show();
        }
        #endregion Расчет маршрута между случайными точками
    }

}

namespace VisibilityTest
{
    public class BooleanToVisibilityConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                var visible = System.Convert.ToBoolean(value, culture);
                if (InvertVisibility)
                    visible = !visible;
                return visible ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Converter cannot convert back.");
        }

        public Boolean InvertVisibility { get; set; }

    }

}