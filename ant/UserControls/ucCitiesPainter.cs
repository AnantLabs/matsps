using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ant.AntAlgData;
using ant.AntAlgLogic;
using ant.CommonData;

namespace ant.UserControls
{
    /// <summary>
    /// Пользовательский контрол, который прорисовывает Коллекцию городов и путь между ними (по порядку следования в коллекции)
    /// </summary>
    public partial class ucCitiesPainter : UserControl
    {
        #region Конструкторы и Данные
        public ucCitiesPainter()                                        
        {
            InitializeComponent();
            
            _brushLiteLine = new SolidBrush(Color.FromArgb( 100, Color.Silver)); // прозрачность 20
            _penLiteLine = new Pen(_brushLiteLine, 1);

            _fontPointsLabel = new Font("Arial", _fontSize );            
            _penCities = new Pen(Brushes.Black, _fCityR *2);
        }

        // Данные городов и маршрутов
        private DataCitiesCollection _cities;

        // Данные состояния контроллов формы
        /// <summary>
        /// Показана или скрыта Панель Свойств
        /// </summary>
        private bool bPanelSettingsShow = true;
        /// <summary>
        /// Последняя высота панели Свойств
        /// </summary>
        private int iPanelSettingsLastHeight;

        // Данные прорисовки
        /// <summary>
        /// Состояние режима прорисовки
        /// </summary>
        private enum DrawingState                                       
        {
            /// <summary>
            /// Прорисовка точек городов
            /// </summary>
            Cities, 
            /// <summary>
            /// Прорисовка линий (дуг) маршрута
            /// </summary>
            Route,
            /// <summary>
            /// Прорисовка городов и дуг
            /// </summary>
            CitiesAndRoute
        }
        private DrawingState stateCurrent = DrawingState.Cities;

        /// <summary>
        /// Кисть городов
        /// </summary>
        private Pen _penCities;   
        private Brush _brushLiteLine;
        private Pen _penLiteLine;
        /// <summary>
        /// Кисть линий маршрутов
        /// </summary>
        private Brush _brushRouteLine;

        private int _fontSize = 9;
        private float _fRouteLineWidth = 3;
        private Font _fontPointsLabel;
        private SolidBrush _brashPointsLabel = new SolidBrush(Color.DarkRed);
        private float _fCityR = (float)2.2; // заданный радиус для точки-города 
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция городов
        /// </summary>
        internal DataCitiesCollection Cities
        {
            set
            {
                dgvRouteList.Rows.Clear();
                RefreshRouteList();
                RefreshRoutePaint();

                _cities = value;
            }
            get
            {
                return _cities;
            }
        }
        /// <summary>
        /// Лист маршрутов
        /// </summary>
        internal List<Route> ListRoute                  
        {
            set;
            get;
        }

        #endregion

        #region Методы | Old
        ///// <summary>
        ///// Прорисовывает коллецию городов
        ///// </summary>
        //public void PaintCities()                                               
        //{
        //    stateCurrent = DrawingState.Cities;
        //    PaintObjects();
        //}

        ///// <summary>
        ///// Прорисовывает маршрут между городами
        ///// </summary>
        //public void PaintRoute()                                                
        //{
        //    stateCurrent = DrawingState.Route;            
        //    PaintObjects();
        //}

        ///// <summary>
        ///// Прорисовка городов и маршрута между ними
        ///// </summary>
        //public void PaintCitiesAndRoute()                                       
        //{
        //    stateCurrent = DrawingState.CitiesAndRoute;
        //    PaintObjects();
        //}

        ///// <summary>
        ///// Перебирает коллекцию городов и прорисовывает объекты по состоянию
        ///// </summary>
        ///// <param name="state">Режим прорисовки: точки или линии</param>
        ///// <param name="pen">Кисть</param>
        //private void PaintObjects()                                             
        //{
        //    int picBoxWidth = pbCanvas.Size.Width;
        //    float fKoefX = (float)picBoxWidth / (float)Cities.MaxDistance;
        //    int picBoxHeight = pbCanvas.Size.Height;
        //    float fKoefY = (float)picBoxHeight / (float)Cities.MaxDistance;
  
        //    pbCanvas.Refresh();
        //    Image img = new Bitmap(picBoxWidth, picBoxHeight);
        //    System.Drawing.Graphics g = Graphics.FromImage(img);

        //    // Прорисовываем линии между всеми городами
        //    if (stateCurrent == DrawingState.CitiesAndRoute ||
        //        stateCurrent == DrawingState.Route || stateCurrent == DrawingState.Cities)
        //    {                
        //        for(int i = 0; i < Cities.Count - 1; i++)
        //            for (int j = i + 1; j < Cities.Count; j++)
        //            {
        //                g.DrawLine(penLiteLine, fKoefX * Cities[i].X, fKoefY * Cities[i].Y, fKoefX * Cities[j].X, fKoefY * Cities[j].Y);
        //            }
        //    }

        //    // Порисовываем путь
        //    for (int j = 0; j < Cities.Count; j++)
        //    {
        //        switch(stateCurrent)
        //        {
        //            case DrawingState.Cities:
        //                g.DrawEllipse(_penCities, fKoefX * Cities[j].X - 2, fKoefY * Cities[j].Y - 2, 4, 4);                        
        //                break;
        //            case DrawingState.Route:
        //                //g.DrawLine(penRouteLine, fKoefX * Route[j].X, fKoefY * Route[j].Y, fKoefX * Route[j + 1].X, fKoefY * Route[j + 1].Y);
        //                break;
        //            case DrawingState.CitiesAndRoute:                        
        //                //g.DrawEllipse(penCities, fKoefX * Route[j].X - 2, fKoefY * Route[j].Y - 2, 4, 4);
        //                //if( j != Cities.Count - 1 )
        //                    //g.DrawLine(penRouteLine, fKoefX * Route[j].X, fKoefY * Route[j].Y, fKoefX * Route[j + 1].X, fKoefY * Route[j + 1].Y);
        //                //g.DrawString((j + 1).ToString(), _fontPointsLabel, _brashPointsLabel, fKoefX * Route[j].X + 3, fKoefY * Route[j].Y - 13);
        //                break;
        //        }
        //    }
        //    // Путь от последнего к первому городу
        //    if( stateCurrent == DrawingState.Route || stateCurrent == DrawingState.CitiesAndRoute)
        //        //g.DrawLine(penRouteLine, fKoefX * Route[Route.Count - 1].X, fKoefY * Route[Route.Count - 1].Y, fKoefX * Route[0].X, fKoefY * Route[0].Y);
            
        //    // Очищаем память
        //    g.Dispose();

        //    pbCanvas.Image = img;
        //}
        #endregion

        #region Методы(внутренние) | New
        // <summary>
        /// обновляем список расчитанных маршрутов в dataGridView
        /// </summary>
        private void UpdateDgvRouteList()                                   
        {
            dgvRouteList.Rows.Clear();

            if (ListRoute.Count > 0)
            {
                for (int i = 0; i < ListRoute.Count; i++)
                {
                    DateTime dt = new DateTime(ListRoute[i].СalcTime.Ticks);

                    dgvRouteList.Rows.Add(
                        ListRoute[i].Drawing.Visible,       // видимость
                        ListRoute[i].UniqueNum,             // номер
                        ListRoute[i].Name,                  // Имя алгоритма, оздавшего маршрут
                        ListRoute[i].Length.ToString("0.###"),                // Длина маршрута
                        ListRoute[i].Drawing.Color.Name,    // Цвет алгоритма (задается отдельно)
                        String.Format("{0}", dt.ToString("mm:ss:fff")),      // Информация о расчете
                        String.Format("{0} - {1}", ListRoute[i].Cities[0].Index + 1, ListRoute[i].Cities[ ListRoute[i].Cities.Count - 1].Index + 1)       // Комментарий (произвольный текст)
                        );
                    DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                    cellStyle.BackColor = ListRoute[i].Drawing.Color;
                    dgvRouteList.Rows[i].Cells[dgvRLcolColor.Name].Style = cellStyle;
                }
            }
        }

        /// <summary>
        /// возвращает ссылку на маршрут по уникальному номеру в нем
        /// </summary>
        /// <param name="number">уникальный номер</param>
        /// <returns>возвращает ссылку на найденный маршрут в листе маршрутов</returns>
        private Route GetRouteFromNumber(int number)                        
        {
            foreach (Route r in ListRoute)
                if (r.UniqueNum == number)
                    return r;
            return null;
        }
        #endregion

        #region Внешние методы
        /// <summary>
        /// Обновить прорисовку в соотвествии с установленными свойствами
        /// </summary>
        public void RefreshRoutePaint()                                     
        {
            int picBoxWidth = 0;
            float fKoefX = 0;
            int picBoxHeight = 0;
            float fKoefY = 0;
            if (_cities != null)
            {
                // Коэффициенты размеров полотна
                picBoxWidth = pbCanvas.Size.Width;
                fKoefX = (float)picBoxWidth / (float)_cities.MaxDistance;
                picBoxHeight = pbCanvas.Size.Height;
                fKoefY = (float)picBoxHeight / (float)_cities.MaxDistance;


                pbCanvas.Refresh();

                System.Drawing.Graphics g;
                Image img;
                // При каждой перерисовке создается новая картинка
                img = new Bitmap(picBoxWidth, picBoxHeight);
                g = Graphics.FromImage(img);


                // Прорисовываем линии между всеми городами
                if (chbGrayLines.Checked)
                    for (int i = 0; i < _cities.Count - 1; i++)
                        for (int j = i + 1; j < _cities.Count; j++)
                            g.DrawLine(_penLiteLine, fKoefX * _cities[i].X, fKoefY * _cities[i].Y, fKoefX * _cities[j].X, fKoefY * _cities[j].Y);


                if (ListRoute.Count > 0)
                {
                    // Прорисовываем отмеченные чекбоксами маршруты
                    for (int i = 0; i < dgvRouteList.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvRouteList.Rows[i].Cells["dgvRLcolVisible"].Value) == true) // отмечен для прорисовки
                        {
                            // Прорисовываем линии между городами пути
                            int index = (int)dgvRouteList.Rows[i].Cells["dgvRLcolNumber"].Value;
                            Route currPaintRoute = GetRouteFromNumber(index); // текущий прорисовываемый маршрут из списка маршрутов

                            _brushRouteLine = new SolidBrush(Color.FromArgb(96, currPaintRoute.Drawing.Color));
                            Pen penRouteLine = new Pen(_brushRouteLine, _fRouteLineWidth);
                            for (int j = 0; j < currPaintRoute.Cities.Count - 1; j++)
                            {                                
                                if (j == 0) // первая линия будет со стрелкой
                                {
                                    penRouteLine = new Pen(_brushRouteLine, _fRouteLineWidth + 4);
                                    penRouteLine.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                                }
                                else
                                {
                                    penRouteLine = new Pen(_brushRouteLine, _fRouteLineWidth);
                                }
                                g.DrawLine(penRouteLine, fKoefX * currPaintRoute.Cities[j].X, fKoefY * currPaintRoute.Cities[j].Y, fKoefX * currPaintRoute.Cities[j + 1].X, fKoefY * currPaintRoute.Cities[j + 1].Y);
                            }
                            // Путь из последнего города в первый
                            g.DrawLine(penRouteLine, fKoefX * currPaintRoute.Cities[currPaintRoute.Cities.Count - 1].X, fKoefY * currPaintRoute.Cities[currPaintRoute.Cities.Count - 1].Y, fKoefX * currPaintRoute.Cities[0].X, fKoefY * currPaintRoute.Cities[0].Y);
                        }
                    }
                }


                // Прорисовываем точки городов с заданной кистью и заданными параметрами            
                for (int j = 0; j < _cities.Count; j++)
                {
                    g.DrawEllipse(_penCities, fKoefX * Cities[j].X - _fCityR, fKoefY * Cities[j].Y - _fCityR, _fCityR * _fCityR, _fCityR * _fCityR);
                    if (chbShowCitiesLabelNumber.Checked) // если отмечен чекбокс
                        g.DrawString((j + 1).ToString(), _fontPointsLabel, _brashPointsLabel, fKoefX * Cities[j].X + _fCityR + 1, fKoefY * Cities[j].Y - _fontSize - _fCityR - 1);
                }

                // Очищаем память
                g.Dispose();

                pbCanvas.Image = img;
            }
        }

        /// <summary>
        /// Обновить таблицу с маршрутами
        /// </summary>
        public void RefreshRouteList()                                      
        {
            // Обновляем Лист Маршрутов  dataGridView
            UpdateDgvRouteList();
        }
        #endregion

        #region События
        /// <summary>
        /// Изменение размеров формы и перерисовка городов
        /// </summary>
        private void ucCitiesPainter_Resize(object sender, EventArgs e)             
        {
            if (Cities != null)
            {
                //PaintObjects();
                RefreshRoutePaint();
            }
        }
        private void ucCitiesPainter_(object sender, EventArgs e)                   
        {
            if (Cities != null)
            {
                //PaintObjects();
                RefreshRoutePaint();
            }
        }

        /// <summary>
        /// Скрыть-показать панель свойств прорисовки
        /// </summary>
        private void btnShowHide_Click(object sender, EventArgs e)                  
        {
            if (bPanelSettingsShow)
            {
                iPanelSettingsLastHeight = splContMain.Panel2.Height;
                splContMain.SplitterDistance = splContMain.Height - btnShowHide.Height;
                //.Panel2.Height = btnShowHide.Height;
                bPanelSettingsShow = false;
            }
            else
            {
                splContMain.SplitterDistance = splContMain.Height - iPanelSettingsLastHeight;
                bPanelSettingsShow = true;
            }
            ucCitiesPainter_Resize(this, new EventArgs());
        }

        private void splContMain_SplitterMoved(object sender, SplitterEventArgs e)  
        {
            try
            {
                RefreshRoutePaint();
            }catch // проглатываем исключение, которое может возникнуть, если  не заполнены данные в момент начальной загрузки
            {}
        }

        /// <summary>
        /// Событие клика мышкой по ячейке таблицы
        /// </summary>
        private void dgvRouteList_MouseUp(object sender, MouseEventArgs e)          
        {
            dgvRouteList.EndEdit();
            DataGridView.HitTestInfo hitInfo = dgvRouteList.HitTest(e.X, e.Y);
            if (hitInfo.ColumnIndex == dgvRLcolVisible.Index) // попали в столбец видимости
            {
                try
                {
                    int uNumber = Convert.ToInt32(dgvRouteList.Rows[hitInfo.RowIndex].Cells[dgvRLcolNumber.Name].Value);

                    Route r = GetRouteFromNumber(uNumber);
                    r.Drawing.Visible = Convert.ToBoolean(dgvRouteList.Rows[hitInfo.RowIndex].Cells[dgvRLcolVisible.Name].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно сопоставить номер в таблице с номером маршрута из памяти");
                }
            }
            RefreshRoutePaint();
        }
        #endregion
    }
}
